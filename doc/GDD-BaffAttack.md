# BaffAttack // GDD

<div style="border: 3px solid #e3e3e3; border-radius: 5px; padding-top: 20px; ">

- Plataforma: PC
- Engine: Godot
- Público alvo: Casual, entusiata de aviação. Amantes de jogos de precisão.
- Controles: mouse ou teclado
</div>

## Game brief

<img src="red-bull-1.jpg">
"BaffAttack" é um jogo inspirado na corrida de aviões da Red Bull (Red Bull Air Race World Series). O jogo dura 5 minutos e o objetivo é, 
quando o tempo acabar, ter a maior quantidade de pontos. Como será falado à frente, é possível aumentar ou diminuir a duração do jogo.

## Jogos similares

- Sky Jinx (Atari 2600)
- Ski Free (Windows 3.1)
- Swing Copters (iOS e Android)

## Tipo

Este é um jogo 2D em terceira pessoa top-down, ou seja, com a vista superior do avião com o terreno em baixo.
Uma câmera virtual mantem o avião no centro da tela com o nariz apontado para a frente. Ou seja,
quando o avião faz uma curva, o cenário que parece girar na direção contrária. Existe uma
suavização na rotação da câmera para que o jogador não tenha uma desorientação espacial, a
imagem de fundo também auxilia nisso.

Também seria possível que a câmera seguisse o jogador sem rotacionar, mas, nos testes preliminares,
esta opção gerou mais confusão. Quando o avião aponta para baixo, os controles de curva ficam
invertidos.

## Controle

É possível aumentar e baixar a velocidade usando o botão "up" e "down" do teclado. O avião também pode realizar curvas. 
Isso pode ser feito usando as setas do teclado; quando a seta é solta, o avião volta para o voo nivelado mantendo a proa constante. Enquanto a tecla da direita é pressionada
o avião aumenta o ângulo de proa em uma razão constante, enquanto a tecla esquerda é pressionada a proa diminui também em razão constante.

A proa pode variar de 0 graus a 359. Ocorre underflow quando o valor fica menor que zero e overflow quando fica maior que 359.

```
                        ╭── +1 ───╮
                        │         v
357        358         359        0        1        2
                        ^         │
                        ╰── -1 ───╯
```

Também é possível usar o mouse; nesse caso, com o cursor na posição central da janela, o voo é mantido nivelado. A distância para a direita ou esquerda determina o quanto a aeronave inclinará para o lado escolhido, permitindo fazer uma curva mais fechada ou aberta. O avião só voltará para o voo nivelado caso o cursor seja recolocado na posição central.

Apenas a coordenada X do mouse importa; a coordenada Y é descartada.

Sendo:

- W: largura da tela;
- R_max: Razão máxima de mudança do ângulo da proa;
- R: Razão atual de mudança do ângulo da proa;
- delta: explicado anteriormente;
- heading: proa atual.

A proa atual é calculada por
`heading += R * delta`

Para se achar o `R` usa-se a posição x do cursor dentro
da tela.

<img src="mouse-x.svg" />


- *(1)* = cursor no canto esquerdo da janela: R = -R\_max
- *(2)* = cursor no meio da janela: R = 0
- *(3)* = cursor no canto direito da janela: R = R\_max

Para valores entre '1 e 2' e '2 e 3' é feita interpolação linear.

## Gameplay

Como na corrida real, o avião deve passar pelos chamados "Air gates", que são duas colunas (pylon na Red Bull Air Race real) pelas quais a aeronave deve passar no meio, exigindo destreza e timing, já que nenhuma parte do avião pode tocar nas colunas. Na competição real, essas colunas são cones de nylon inflados com ar, como balões que não causam danos à aeronave em caso de colisão, apenas o material se rasga, indicando que o avião não passou corretamente.

<img src="red-bull-2.jpg">

## Air Gate

Existem dois tipos de Air gates: o largo e o estreito.

### Largo

No largo, o avião deve passar sem tocar em nenhuma parte nas colunas. Caso toque, são descontados 100 pontos. Existe um sentido correto para passar; caso passe no sentido errado, são perdidos 500 pontos.

### Estreito

No Air gate estreito, as regras anteriores também valem, mas uma dificuldade extra é adicionada. Quando estiver passando, o avião deve estar com as
asas inclinadas, mais precisamente com `abs(R) >= R_max / 2`. O significado de R e R\_max foi explicado anteriormente. 
Caso esta regra seja descumprida, são descontados 50 pontos. O desconto é cumulativo, então, caso o avião passe em voo nivelado e atinja um dos cones, são perdidos 150 pontos.

## Power up/down

O jogo dura 5 minutos, mas aparecem em posições aleatórias do mapa "power-ups" de tempos que dão 30 segundos extras caso o avião passe por cima de um. 
Este possui a cor verde. Caso esteja na cor vermelha é descontado 1 minuto. Ambos os power-ups desaparecem ao serem passados.

## Caminho
O avião deve seguir a ordem de "airgates" mantendo-se dentro de um caminho pre-estabelecido. A cada delta, a distância do avião
ao caminho é calculada. A partir de um limiar, o jogador
começa a perder ponto em uma razão proporcional à distância do caminho. Caso ele fique muito longe, em poucos segundos
seu escore chega a zero e ele perde.

## Término do jogo

O jogo termina se uma ou mais destas condições ocorrerem:

1) O tempo chegar em zero;
2) os pontos do jogador ficarem iguais a zero ou negativos.