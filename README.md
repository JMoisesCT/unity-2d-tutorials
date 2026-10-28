# unity-2d-tutorials

Proyecto de Unity que acompaña a una serie de cursos de C# con Unity para personas que empiezan a programar. Los cursos usan la rana ninja de Pixel Adventure 1, salvo Curvas y velocidad y Arrastrar y soltar, que usan el arte de Kenney, Top-down, que usa el de 0x72, y Beat 'em up, que usa el de Ansimuz.

Cada curso vive en su propia rama, y cada sesión tiene un tag con el estado exacto del proyecto al terminarla. Sirve para comparar tu proyecto con el del curso o para empezar una sesión desde cero.

## Cursos

| Curso | Rama | Tags | Carpeta |
|---|---|---|---|
| C# con Unity (curso base, 8 sesiones) | `main` | `sesion-01` … `sesion-08` | `Assets/Game/` |
| Plataformas 2D (8 sesiones) | `plataformas-1` | `plataformas-1-sesion-01` … `plataformas-1-sesion-08` | `Assets/Plataformas/` |
| Plataformas 2D 2: enemigos y un juego completo (7 sesiones) | `plataformas-2` | `plataformas-2-sesion-01` … `plataformas-2-sesion-07` | `Assets/Plataformas/` |
| Curvas y velocidad (10 sesiones) | `plat-curvas-1` | `plat-curvas-1-sesion-01` … `plat-curvas-1-sesion-10` | `Assets/Curvas/` |
| Arrastrar y soltar (8 sesiones) | `clics-1` | `clics-1-sesion-01` … `clics-1-sesion-08` | `Assets/Clics/` |
| Top-down: héroes que cambian de forma (9 sesiones) | `topdown-1` | `topdown-1-sesion-01` … `topdown-1-sesion-09` | `Assets/TopDown/` |
| Beat 'em up: la calle y sus peleadores (4 sesiones) | `beatemup-1` | `beatemup-1-sesion-01` … `beatemup-1-sesion-04` | `Assets/BeatEmUp/` |
| Beat 'em up 2: golpes y combos (3 sesiones) | `beatemup-2` | `beatemup-2-sesion-01` … `beatemup-2-sesion-03` | `Assets/BeatEmUp/` |
| Beat 'em up 3: vida (3 sesiones) | `beatemup-3` | `beatemup-3-sesion-01` … `beatemup-3-sesion-03` | `Assets/BeatEmUp/` |
| Beat 'em up 4: una calle completa (7 sesiones) | `beatemup-4` | `beatemup-4-sesion-01` … `beatemup-4-sesion-07` | `Assets/BeatEmUp/` |

El curso de Plataformas 2D parte del final del curso base (`sesion-08`), y Plataformas 2D 2 continúa donde termina Plataformas 2D (`plataformas-1-sesion-08`). Curvas y velocidad, Arrastrar y soltar, Top-down y Beat 'em up también parten de `sesion-08` y no dependen de los otros cursos. Cada Beat 'em up siguiente continúa donde termina el anterior (`beatemup-1-sesion-04`, `beatemup-2-sesion-03` y `beatemup-3-sesion-03`).

## Cómo usarlo

1. En GitHub, elige el tag de la sesión que quieres (selector de ramas › **Tags**).
2. Botón **Code › Download ZIP** y descomprímelo.
3. En Unity Hub, **Add › Add project from disk** y elige la carpeta.
4. Abre la escena del curso: `Assets/Game/Scenes/` (curso base) o `Assets/Plataformas/Scenes/Nivel1.unity` (Plataformas 2D; en Plataformas 2D 2, desde la sesión 5, `Menu.unity`) o `Assets/Curvas/Scenes/` (Curvas y velocidad; al final, `NivelAlien.unity` y `NivelCaracol.unity`) o `Assets/Clics/Scenes/Nivel1.unity` (Arrastrar y soltar) o `Assets/TopDown/Scenes/Mazmorra.unity` (Top-down) o `Assets/BeatEmUp/Scenes/Calle.unity` (Beat 'em up).

## Requisitos

- Unity 6.3 (6000.3.13f1), plantilla Universal 2D.

## Créditos

- Arte: Pixel Adventure 1, de Pixel Frog (Unity Asset Store).
- Arte de Curvas y velocidad y de Arrastrar y soltar: Platformer Pack Remastered, de Kenney (kenney.nl, CC0).
- Arte de Top-down: 16x16 DungeonTileset II, de 0x72 (0x72.itch.io, CC0).
- Arte de Beat 'em up: Streets of Fight, de Ansimuz (ansimuz.itch.io, CC0).
