# unity-2d-tutorials

Proyecto de Unity que acompaña a una serie de cursos de C# con Unity para personas que empiezan a programar. Los cursos usan la rana ninja de Pixel Adventure 1, salvo Curvas y velocidad, que usa el arte de Kenney.

Cada curso vive en su propia rama, y cada sesión tiene un tag con el estado exacto del proyecto al terminarla. Sirve para comparar tu proyecto con el del curso o para empezar una sesión desde cero.

## Cursos

| Curso | Rama | Tags | Carpeta |
|---|---|---|---|
| C# con Unity (curso base, 8 sesiones) | `main` | `sesion-01` … `sesion-08` | `Assets/Game/` |
| Plataformas 2D (8 sesiones) | `plataformas-1` | `plataformas-1-sesion-01` … `plataformas-1-sesion-08` | `Assets/Plataformas/` |
| Plataformas 2D 2: enemigos y un juego completo (7 sesiones) | `plataformas-2` | `plataformas-2-sesion-01` … `plataformas-2-sesion-07` | `Assets/Plataformas/` |
| Curvas y velocidad (10 sesiones) | `plat-curvas-1` | `plat-curvas-1-sesion-01` … `plat-curvas-1-sesion-10` | `Assets/Curvas/` |

El curso de Plataformas 2D parte del final del curso base (`sesion-08`), y Plataformas 2D 2 continúa donde termina Plataformas 2D (`plataformas-1-sesion-08`). Curvas y velocidad también parte de `sesion-08` y no depende de los cursos de Plataformas.

## Cómo usarlo

1. En GitHub, elige el tag de la sesión que quieres (selector de ramas › **Tags**).
2. Botón **Code › Download ZIP** y descomprímelo.
3. En Unity Hub, **Add › Add project from disk** y elige la carpeta.
4. Abre la escena del curso: `Assets/Game/Scenes/` (curso base) o `Assets/Plataformas/Scenes/Nivel1.unity` (Plataformas 2D; en Plataformas 2D 2, desde la sesión 5, `Menu.unity`) o `Assets/Curvas/Scenes/` (Curvas y velocidad; al final, `NivelAlien.unity` y `NivelCaracol.unity`).

## Requisitos

- Unity 6.3 (6000.3.13f1), plantilla Universal 2D.

## Créditos

- Arte: Pixel Adventure 1, de Pixel Frog (Unity Asset Store).
- Arte de Curvas y velocidad: Platformer Pack Remastered, de Kenney (kenney.nl, CC0).
