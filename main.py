from unicurses import *
from zipfile import ZipFile, ZIP_DEFLATED
from PIL import Image
from io import BytesIO
from json import load, dump
from sys import exit
from os import listdir, system, remove, path


def common_handler():
    text = convert_to_ascii(image)
    with open('output.txt', 'w') as file:
        file.write(text)

    if print_result is True:
        initscr()
        curs_set(0)

        mvaddstr(0, 0, text.replace('\n', ''))
        getch()
        endwin()


def gif_handler():
    memfile = BytesIO()
    with ZipFile(memfile, 'w', compression = ZIP_DEFLATED) as zipfile:
        output = []
        for frame in range(image.n_frames):
            image.seek(frame)
            ascii = convert_to_ascii(image)

            output.append(ascii)
            zipfile.writestr(
                f'output{frame + 1}.txt', str.encode(ascii, 'utf-8')
            )

    with open('output.zip', 'wb') as file:
        file.write(memfile.getvalue())

    if print_result is True:
        initscr()
        timeout(ms)
        curs_set(0)
        noecho()

        while True:
            for text in output:
                mvaddstr(0, 0, text.replace('\n', ''))
                if getch() is ERR: refresh()
                else:
                    endwin()
                    return


def convert_to_ascii(image):
    image  = image.resize((width, height)).convert('L')
    rawtxt = ''.join([chars[int(color / dividend)] for color in image.getdata()])
    return '\n'.join([rawtxt[i:(i + width)] for i in range(0, len(rawtxt), width)])


defaultconfig = {
    'fps': 24,
    'width': 80,
    'height': 40,
    'reverse_background': False,
    'print_result': True,
    'characters': ' _,.:;-~+=*!?/[(&$#@'
}

directory = path.dirname(path.realpath(__file__))
files = listdir(directory)

defaultfiles = (path.basename(__file__), 'settings.json', 'output.zip', 'output.txt', 'run.bat', '__pycache__')
invalidfiles = [f for f in files if not f.endswith(('.gif', '.jpg', '.png')) and f not in defaultfiles]

if invalidfiles: exit('Insira este arquivo em uma pasta vazia.')
if 'settings.json' not in files:
    with open('settings.json', 'w') as json:
        dump(defaultconfig, json, indent = 4)

try:
    with open('settings.json') as data:
        userconfig = load(data)
        if (
            {k: type(v) for k, v in defaultconfig.items()} != {k: type(v) for k, v in userconfig.items()}
            or not 201 > userconfig.get('width', -1) > 0
        ): exit('O arquivo de configurações foi modificado incorretamente.')
except FileNotFoundError: exit('O arquivo de configurações foi deletado.')

fps, width, height, reverse_chars, print_result, chars = userconfig.values()
system(f'MODE {width}, {height}')

imgpath = input('Insira um caminho válido para a imagem: ')
try: image = Image.open(imgpath)
except: exit('Arquivo inválido.')

ms = round(1000 / fps)

if not height:
    original_height, original_width = image.size
    height = int(width * original_height / original_width)

if reverse_chars is True: chars = chars[::-1]
dividend = 255 / (len(chars) - 1)

for file in [l for l in files if l in ('output.zip', 'output.txt')]:
    remove(path.join(directory, file))

try:
    match imgpath[imgpath.rfind('.') + 1:]: #{
        case 'gif': gif_handler()
        case 'jpg' | 'png': common_handler()
        case _: exit('Formato não suportado.')
    #}
except: pass
system('cls || clear')
