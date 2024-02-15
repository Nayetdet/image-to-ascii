from unicurses import *
from zipfile import ZipFile, ZIP_DEFLATED
from PIL import Image
from io import BytesIO
from json import load, dump
from sys import exit
from os import listdir, system, remove, path

class ConfigurationManager:
    DEFAULT_CONFIG = {
        'fps': 24,
        'width': 80,
        'height': 40,
        'reverse_background': False,
        'print_result': True,
        'characters': ' _,.:;-~+=*!?/[(&$#@'
    }

    def __init__(self):
        self.directory = path.dirname(path.realpath(__file__))
        self.files = listdir(self.directory)

        self.get_user_config()
        self.get_user_image()

        if self.reverse_chars is True: self.chars = self.chars[::-1]
        if not self.height:
            original_height, original_width = self.image.size
            self.height = int(self.width * original_height / original_width)

        self.clean_up_previous_files()

    def check_current_directory(self):
        expected_files = [path.basename(__file__), 'run.bat', 'README.md', 'output.zip', 'output.txt', 'settings.json', 'requirements.txt', '__pycache__']
        unexpected_files = [file for file in self.files if not file.endswith(('.gif', '.jpg', '.png')) and file not in expected_files]
        if unexpected_files:
            exit('Insira este arquivo em uma pasta vazia.')

    def get_user_config(self):
        self.check_current_directory()
        if 'settings.json' not in self.files:
            with open('settings.json', 'w') as json:
                dump(self.DEFAULT_CONFIG, json, indent = 4)
        try:
            with open('settings.json') as data:
                user_config = load(data)
                if (
                    {k: type(v) for k, v in self.DEFAULT_CONFIG.items()} != {k: type(v) for k, v in user_config.items()}
                    or not 201 > user_config.get('width', -1) > 0
                ): exit('O arquivo de configurações foi modificado incorretamente.')
        except FileNotFoundError: exit('O arquivo de configurações foi deletado.')
        self.fps, self.width, self.height, self.reverse_chars, self.print_result, self.chars = user_config.values()
        self.ms = round(1000 / self.fps)

    def get_user_image(self):
        system(f'MODE {self.width}, {self.height}')
        self.img_path = input('Insira um caminho válido para a imagem: ')
        try: self.image = Image.open(self.img_path)
        except: exit('Arquivo inválido.')

    def clean_up_previous_files(self):
        for file in self.files:
            if file in ('output.zip', 'output.txt'):
                remove(path.join(self.directory, file))

class ASCIIArtGenerator(ConfigurationManager):
    def __init__(self):
        super().__init__()
        self.select_processing_method()

    def handle_image_conversion(self):
        text = self.convert_to_ascii(self.image)
        with open('output.txt', 'w') as file:
            file.write(text)

        if self.print_result is True:
            initscr()
            curs_set(0)

            mvaddstr(0, 0, text.replace('\n', ''))
            getch()
            endwin()

    def handle_gif_conversion(self):
        memfile = BytesIO()
        with ZipFile(memfile, 'w', compression = ZIP_DEFLATED) as zipfile:
            output = []
            for frame in range(self.image.n_frames):
                self.image.seek(frame)
                ascii = self.convert_to_ascii(self.image)

                output.append(ascii)
                zipfile.writestr(
                    f'output{frame + 1}.txt', str.encode(ascii, 'utf-8')
                )

        with open('output.zip', 'wb') as file:
            file.write(memfile.getvalue())

        if self.print_result is True:
            initscr()
            timeout(self.ms)
            curs_set(0)
            noecho()

            while True:
                for text in output:
                    mvaddstr(0, 0, text.replace('\n', ''))
                    if getch() is ERR: refresh()
                    else:
                        endwin()
                        return

    def convert_to_ascii(self, image):
        image = image.resize((self.width, self.height)).convert('L')
        raw_text = ''.join([self.chars[int(color / (255 / (len(self.chars) - 1)))] for color in image.getdata()])
        return '\n'.join([raw_text[i:(i + self.width)] for i in range(0, len(raw_text), self.width)])

    def select_processing_method(self):
        try:
            match self.img_path[self.img_path.rfind('.') + 1:]:
                case 'gif': self.handle_gif_conversion()
                case 'jpg' | 'png': self.handle_image_conversion()
                case _: exit('Formato não suportado.')
        except: pass
        system('cls || clear')

if __name__ == '__main__':
    ASCIIArtGenerator()
