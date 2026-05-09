# U002_Militar_Invasion_2D

[English](README.en.md) | [Español](README.md)

## Resumen

Prototipo arcade 2D inspirado en la estructura clásica de juegos tipo *Space Invaders*, desarrollado en Unity con C#. El jugador controla un robot en un escenario fijo visto desde arriba y debe eliminar enemigos de temática militar antes de perder todas sus vidas.

El proyecto incluye varios niveles base, un sistema de desbloqueo progresivo, enemigos con IA básica, proyectiles del jugador y de los enemigos, puntuación, vidas, victoria, derrota y un nivel boss contra una araña mecha con habilidades especiales.

## Tecnologías

- Unity
- C#
- Sistema de físicas 2D de Unity
- Collider2D / Rigidbody2D
- Particle System
- Animator
- UI básica
- AudioSource
- Tilemap
- Sistema de vidas
- Sistema de puntuación
- Sistema de niveles
- Git LFS
- GitHub Releases

## Características principales

- Juego arcade 2D con vista superior.
- Escenario fijo sin scroll.
- Movimiento horizontal del jugador.
- Disparo del jugador mediante tecla espacio.
- Balas y proyectiles del jugador y de los enemigos.
- Enemigos de temática militar: soldados, coches, tanques y otras unidades.
- IA básica para enemigos comunes.
- IA mejorada para boss.
- Nivel boss contra una araña mecha con habilidades especiales.
- Sistema de vidas del jugador.
- Sistema de vida para enemigos.
- Sistema de puntuación.
- Sistema de niveles.
- Victoria al eliminar todos los enemigos.
- Derrota al perder todas las vidas.
- Desbloqueo de niveles al superar el anterior.
- Menú principal y menú de selección de niveles.
- Partículas, sonido y música.
- Build jugable para Windows.

## Capturas

> Pendiente de añadir capturas finales.

Ruta prevista:

![Gameplay](../Media/screenshots/gameplay-01.png)

## Arquitectura

La lógica principal se divide en:

- `AnimControl` — control de animaciones, partículas y feedback visual.
- `BulletControl` — gestión de proyectiles del jugador y enemigos.
- `EnemyCreator` — creación de enemigos y organización de oleadas o formaciones.
- `GameManager` — control de partida, puntuación, vidas, niveles, victoria y derrota.
- `WorldCreator` — generación o preparación del escenario y estructura del nivel.

## Código recomendado para revisar

[`Project/Assets/Scripts/GameManager.cs`](./Project/PRJ_MilitarInvasion/Assets/Scripts/GameManager.cs)

## Build

La build está disponible en GitHub Releases.

[`Releases/Download.md`](../Releases/Download.md)

[Descargar build U002-v1.0.0](https://github.com/BLRochaGonzalez93/U002_Militar_Invasion_2D/releases/tag/U002-v1.0.0)

## Estado

**Prototipo base jugable.**

El proyecto contiene cuatro niveles base y un nivel boss contra una araña mecha. La demo incluye movimiento horizontal, disparo, enemigos, proyectiles, vidas, puntuación, niveles, victoria, derrota, desbloqueo de niveles, menú principal, menú de niveles, partículas, sonido y música.

Pendiente de posibles mejoras:

- Añadir más niveles.
- Añadir distintos tipos de enemigos.
- Añadir sistema de pausa.
- Añadir guardado de puntuación máxima.
- Mejorar y ampliar el desbloqueo de niveles por superación del nivel anterior.
- Añadir habilidades.
- Añadir power-ups.
- Añadir árbol de habilidades.

## Aprendizajes

Este proyecto me permitió practicar movimiento y disparo 2D, gestión de proyectiles y colisiones entre balas, enemigos y jugador.

También me sirvió para trabajar estados básicos de partida, incluyendo puntuación, vidas, victoria, derrota y progresión por niveles.

Además, el proyecto me ayudó a organizar un prototipo arcade de acción separando responsabilidades entre creación de enemigos, control de proyectiles, control de mundo, animaciones y gestión global de la partida.
