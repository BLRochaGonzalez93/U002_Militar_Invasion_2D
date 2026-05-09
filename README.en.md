# U002_Militar_Invasion_2D

[English](README.en.md) | [Español](README.md)

## Summary

**Militar Invasion 2D** is a 2D arcade action prototype inspired by the classic structure of *Space Invaders*-style games, developed in Unity with C#. The player controls a robot in a fixed top-down scenario, moving horizontally to dodge attacks and shoot against military-themed enemy waves.

Enemies include soldiers, cars, tanks and other hostile units or vehicles. The player wins by eliminating all enemies in the level and loses if all lives are depleted. The project includes several base levels, progressive level unlocking and a boss level against a mecha spider with special abilities.

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

- 2D arcade gameplay inspired by classic enemy-invasion logic.
- Top-down fixed-screen view.
- Horizontal player movement without leaving the screen.
- Shooting controlled through the space key.
- Player and enemy bullets/projectiles.
- Enemies with basic AI.
- Final mecha-spider boss with improved AI and special abilities.
- Player lives system.
- Enemy health system.
- Scoring system.
- Level system.
- Victory and defeat conditions.
- Level unlocking after completing the previous level.
- Main menu and level selection menu.
- Particles, sound and music.
- Playable Windows build.

## Visuals

> Final screenshots and images pending.

Planned visual pack names:

- `militarinvasion-logo.png`
- `militarinvasion-cover.png`
- `militarinvasion-banner.png`
- `militarinvasion-thumbnail-01-player-shooting.png`
- `militarinvasion-thumbnail-02-enemy-waves.png`
- `militarinvasion-thumbnail-03-mecha-spider-boss.png`
- `militarinvasion-thumbnail-04-level-selection.png`

## Architecture

The main logic is divided into:

- `AnimControl` — animation, effects and visual feedback control.
- `BulletControl` — bullet and projectile management.
- `EnemyCreator` — enemy creation and organization.
- `GameManager` — global game control, levels, victory, defeat, lives and score.
- `WorldCreator` — level structure creation or setup.

## Recommended code to review

[`Project/Assets/Scripts/GameManager.cs`](./Project/Assets/Scripts/GameManager.cs)

## Build

The build is available through GitHub Releases.

[Download build U002-v1.0.0](https://github.com/BLRochaGonzalez93/U002_Militar_Invasion_2D/releases/tag/U002-v1.0.0)

## Status

**Playable base prototype.**

The project includes four base levels and one boss level against a mecha spider. It contains horizontal player movement, shooting, enemies, projectiles, lives, score, levels, victory, defeat, level unlocking, sound, music and menus.

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

This project allowed me to work on 2D arcade mechanics based on horizontal movement, shooting, projectiles and enemy waves.

It also helped me practice collision management between bullets, enemies and the player, as well as basic game-state control: victory, defeat, score, lives and level progression.

In addition, the project helped me organize an action prototype with a clear manager-based structure, separation of responsibilities and basic progression toward more complex levels, including a boss fight.
