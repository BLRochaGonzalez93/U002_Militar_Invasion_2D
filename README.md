# U002_Militar_Invasion_2D

[English](README.en.md) | [Español](README.md)

## Resumen

**Militar Invasion 2D** es un prototipo arcade de acción 2D inspirado en la estructura clásica de juegos tipo *Space Invaders*, desarrollado en Unity con C#. El jugador controla un robot en un escenario fijo visto completamente desde arriba, desplazándose horizontalmente para esquivar ataques y disparar contra oleadas de enemigos de temática militar.

Los enemigos incluyen soldados, coches, tanques y otros vehículos o unidades hostiles. El jugador gana al eliminar todos los enemigos del nivel y pierde si se queda sin vidas. El proyecto incluye varios niveles base, desbloqueo progresivo de niveles y un nivel boss contra una araña mecha con habilidades especiales.

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

- Gameplay arcade 2D inspirado en la lógica clásica de invasión de enemigos.
- Vista superior con escenario fijo.
- Movimiento horizontal del jugador sin salida de pantalla.
- Control de disparo mediante tecla espacio.
- Balas y proyectiles tanto del jugador como de los enemigos.
- Enemigos con IA básica.
- Boss final tipo araña mecha con IA mejorada y habilidades especiales.
- Sistema de vidas del jugador.
- Sistema de vida para enemigos.
- Sistema de puntuación.
- Sistema de niveles.
- Condiciones de victoria y derrota.
- Desbloqueo de niveles al superar el anterior.
- Menú principal y menú de selección de niveles.
- Partículas, sonido y música.
- Build jugable para Windows.

## Visuales

> Pendiente de añadir capturas e imágenes finales.

Nombres previstos para el pack visual:

- `militarinvasion-logo.png`
- `militarinvasion-cover.png`
- `militarinvasion-banner.png`
- `militarinvasion-thumbnail-01-player-shooting.png`
- `militarinvasion-thumbnail-02-enemy-waves.png`
- `militarinvasion-thumbnail-03-mecha-spider-boss.png`
- `militarinvasion-thumbnail-04-level-selection.png`

## Arquitectura

La lógica principal se divide en:

- `AnimControl` — control de animaciones, efectos y feedback visual.
- `BulletControl` — gestión de balas y proyectiles.
- `EnemyCreator` — creación y organización de enemigos.
- `GameManager` — control global de partida, niveles, victoria, derrota, vidas y puntuación.
- `WorldCreator` — creación o preparación de la estructura del nivel.

## Código recomendado para revisar

[`Project/Assets/Scripts/GameManager.cs`](./Project/Assets/Scripts/GameManager.cs)

## Build

La build está disponible en GitHub Releases.

[Descargar build U002-v1.0.0](https://github.com/BLRochaGonzalez93/U002_Militar_Invasion_2D/releases/tag/U002-v1.0.0)

## Estado

**Prototipo base jugable.**

El proyecto incluye cuatro niveles base y un nivel boss contra una araña mecha. Contiene movimiento horizontal del jugador, disparo, enemigos, proyectiles, vidas, puntuación, niveles, victoria, derrota, desbloqueo de niveles, sonido, música y menús.

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

Este proyecto me permitió trabajar mecánicas arcade 2D basadas en movimiento horizontal, disparo, proyectiles y oleadas de enemigos.

También me ayudó a practicar la gestión de colisiones entre balas, enemigos y jugador, así como el control de estados básicos de partida: victoria, derrota, puntuación, vidas y avance por niveles.

Además, el proyecto sirvió para organizar un prototipo de acción con una estructura clara de managers, separación de responsabilidades y una progresión básica hacia niveles más complejos, incluyendo un combate contra boss.
