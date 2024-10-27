# Imagem para ASCII
Este é um código que transforma qualquer imagem ou GIF em uma representação visual única e estilizada usando caracteres ASCII.

## Instalação e Utilização
Para utilizar esse código, é simples. Coloque cada um dos arquivos desta página em uma PASTA VAZIA, junto com alguns arquivos nos formatos png, jpg ou gif. Em seguida, inicie o executável do arquivo e insira o nome da imagem desejada quando solicitado.

## Configurações Personalizadas
Ao ser executado pela primeira vez, o programa cria um arquivo de configurações JSON (`settings.json`). Este arquivo pode ser modificado posteriormente pelo usuário para ajustar as preferências:

```
"Characters": " _,.:;-~=*!?/[($#@"
"ScreenWidth": 80,
"ScreenHeight": 40,
"Fps": 12,
```
> - **Characters:** Uma string que contém os caracteres que serão usados para representar diferentes tons de cinza na imagem ASCII. Cada caractere representa um nível de cinza, do mais escuro ao mais claro. <br />
> - **ScreenWidth:** Representa a largura desejada para a arte ASCII gerada. <br />
> - **ScreenHeight:** Representa a altura desejada para a arte ASCII gerada. <br />
> - **Fps:** Indica o número de frames (quadros) por segundo na animação ASCII gerada a partir de um arquivo GIF. <br />

## Output alternativo
Outro ponto crucial a ser destacado é que, durante o processamento da imagem, é gerado um arquivo .txt (no caso de imagens) ou um arquivo .zip (no caso de GIFs), proporcionando ao usuário fácil acesso aos resultados obtidos.
