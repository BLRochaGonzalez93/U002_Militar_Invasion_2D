# U002_Militar_Invasion_2D

[English](README.en.md) | [Español](README.md)

## Summary

2D arcade prototype inspired by the classic structure of *Space Invaders*-style games, developed in Unity with C#. The player controls a robot in a fixed top-down scenario and must eliminate military-themed enemies before losing all lives.

The project includes several base levels, a progressive unlocking system, enemies with basic AI, player and enemy projectiles, score, lives, victory, defeat and a boss level against a mecha spider with special abilities.

## Technologies

- Unity
- C#
- Unity 2D physics system
- Collider2D / Rigidbody2D
- Particle System
- Animator
- Basic UI
- AudioSource
- Tilemap
- Lives system
- Scoring system
- Level system
- Git LFS
- GitHub Releases

## Main features

- 2D arcade game with a top-down view.
- Fixed scenario without scrolling.
- Horizontal player movement.
- Player shooting using the space key.
- Player and enemy bullets/projectiles.
- Military-themed enemies: soldiers, cars, tanks and other units.
- Basic AI for common enemies.
- Improved AI for the boss.
- Boss level against a mecha spider with special abilities.
- Player lives system.
- Enemy health system.
- Scoring system.
- Level system.
- Victory by eliminating all enemies.
- Defeat after losing all lives.
- Level unlocking after completing the previous level.
- Main menu and level selection menu.
- Particles, sound and music.
- Playable Windows build.

## Screenshots

> Final screenshots pending.

Planned path:

![Gameplay](../Media/screenshots/gameplay-01.png)

## Architecture

The main logic is divided into:

- `AnimControl` — animations, particles and visual feedback control.
- `BulletControl` — player and enemy projectile management.
- `EnemyCreator` — enemy creation and organization of waves or formations.
- `GameManager` — game control, score, lives, levels, victory and defeat.
- `WorldCreator` — scenario generation or level-structure setup.

## Recommended code to review

[`PRJ_MilitarInvasion/Assets/Scripts/GameManager.cs`](./PRJ_MilitarInvasion/Assets/Scripts/GameManager.cs)

## Build

The build is available through GitHub Releases.

[`Releases/Download.md`](../Releases/Download.md)

[Download build U002-v1.0.0](https://github.com/BLRochaGonzalez93/U002_Militar_Invasion_2D/releases/tag/U002-v1.0.0)

## Status

**Playable base prototype.**

The project contains four base levels and one boss level against a mecha spider. The demo includes horizontal movement, shooting, enemies, projectiles, lives, score, levels, victory, defeat, level unlocking, main menu, level menu, particles, sound and music.

Possible pending improvements:

- Add more levels.
- Add different enemy types.
- Add a pause system.
- Add high-score saving.
- Improve and expand level unlocking after completing the previous level.
- Add abilities.
- Add power-ups.
- Add a skill tree.

## Learnings

This project allowed me to practice 2D movement and shooting, projectile management and collisions between bullets, enemies and the player.

It also helped me work on basic game states, including score, lives, victory, defeat and level progression.

In addition, the project helped me organize an arcade action prototype by separating responsibilities between enemy creation, projectile control, world control, animations and global game management.
