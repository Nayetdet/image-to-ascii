# Imagem para ASCII
Este é um código que transforma qualquer imagem ou gif em uma representação visual única e estilizada usando caracteres ASCII.

## Pré-Requisitos
Antes de prosseguir com as instruções abaixo, certifique-se de que você possui uma versão atualizada do Python e instalou todos os requisitos listados no arquivo requirements.txt. É importante destacar que este programa oferece suporte exclusivamente para o sistema operacional Windows.

## Instalação e Utilização
Para utilizar esse código, é simples. Coloque cada um dos arquivos desta página em uma PASTA VAZIA, junto com alguns arquivos nos formatos png, jpg ou gif. Em seguida, inicialize o arquivo run.bat e insira o nome da imagem desejada quando solicitado.

## Configurações Personalizadas
Ao ser executado pela primeira vez, o programa cria um arquivo de configurações JSON (`settings.json`). Este arquivo pode ser modificado posteriormente pelo usuário para ajustar as preferências:

```
    "fps": 24,
    "width": 80,
    "height": 40,
    "reverse_background": false,
    "print_result": true,
    "characters": " _,.:;-~+=*!?/[(&$#@"
```
> - **fps:** Indica o número de frames (quadros) por segundo na animação ASCII gerada a partir de um arquivo GIF. <br />
> - **width:** Representa a largura desejada para a arte ASCII gerada. <br />
> - **height:** Representa a altura desejada para a arte ASCII gerada. <br />
> - **reverse_background:** Indica se o fundo da imagem ASCII gerada deve ser invertido ou não. <br />
> - **print_result:** Indica se o resultado (a imagem ASCII) deve ser impresso no console ou não. <br />
> - **characters:** Uma string que contém os caracteres que serão usados para representar diferentes tons de cinza na imagem ASCII. Cada caractere representa um nível de cinza, do mais escuro ao mais claro. A ordem desses caracteres pode ser invertida se a opção reverse_background estiver ativada. <br />

### Output alternativo
Outro ponto crucial a ser destacado é que, durante o processamento da imagem, é gerado um arquivo .txt (no caso de imagens) ou um arquivo .zip (no caso de GIFs), proporcionando ao usuário fácil acesso aos resultados obtidos.
