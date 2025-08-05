# Blocky's Escape: A Digital Odyssey 🧱

## Break Free from the Pixels, Discover Your Reality\!

Blocky's Escape is a nostalgic 2D platformer where you guide Blocky, a pixelated hero, on a profound journey to escape the digital confines of his world and uncover the truth of his existence. Master precise jumps, outsmart quirky enemies, collect vital coins, and navigate a branching narrative that leads to multiple, impactful endings.

-----

## 🎮 Gameplay

Navigate your blocky avatar through diverse, side-scrolling levels filled with obstacles, hazards, and enemies. Your goal is to skillfully traverse platforms, collect scattered coins, and reach the end of each stage. Beyond simple progression, your choices and performance will influence Blocky's ultimate fate, leading to different narrative conclusions. Prepare for timed challenges, boss encounters, and the thrill of uncovering hidden secrets\!

-----

## ✨ Features

  * **Responsive Movement:** Control Blocky with **tight horizontal movement**, **precise jumping**, and a **dynamic sprint** that smoothly adjusts speed.

  * **Rotational Control:** Rotate Blocky using the Up/Down arrow keys for unique interactions.

  * **Lives & Respawn System:** Manage your lives carefully\! Respawn at designated points upon falling off-screen, touching hazards, or taking enemy hits.

  * **Temporary Invincibility:** Gain a brief period of **invincibility** after taking damage or respawning, clearly indicated by **sprite blinking**.

  * **Dynamic Sprites:** Blocky's appearance changes temporarily upon **coin collection** 💰, **door entry** 🚪, and after **taking damage** 🤕. His default movement animates by alternating between two distinct sprites.

  * **Audio Feedback:** Enjoy satisfying sound effects for **jumps** 🔊, **coin pickups** ✨, and **enemy defeats**💥.

  * **Varied Enemy AI:**

      * **Ground Enemies:** Patrol set distances, flipping their sprite to match movement. They activate only when visible on camera for optimized performance.
      * **Airborne Enemies:** Aggressively **seek and chase** Blocky, flying continuously at high speed. Their sprite flips to face the player's direction.

  * **Multi-Hit Defeat:** Specific enemies (like the transformed NPC) require **multiple hits** to be defeated.

  * **Damage & Invincibility:** Enemies flash their sprite when hit and gain a temporary invincibility window.

  * **NPC Transformation:** A key NPC transforms into a hostile flying enemy under specific conditions, marked by **sprite darkening** ⚫ and **particle effects** ✨.

  * **Persistent Data:** Your **score**, **coins collected**, **current level**, and **lives** are all saved and loaded across different scenes using PlayerPrefs, ensuring continuous progress.

  * **Bonus Lives:** Earn an **extra life** ❤️ when your score first crosses **1000, 2000, and 3000 points**.

  * **Timed Levels:** Each level includes a **countdown timer** ⏳, leading to consequences if time runs out.

  * **Dynamic Level Elements:** **Secret tilemaps** can be disabled and **new doors** revealed based on coin collection milestones and specific NPC interactions.

  * **Scene Transitions:** Seamlessly load subsequent levels, a secret level, and various ending scenes.

  * **Text-Based Cutscenes:** The `CutsceneManager` delivers story segments between levels, featuring a **typewriter effect** ✍️ and **text fade-out** 👻 before scene transitions. Cutscenes are context-dependent, playing different narratives based on the active scene.

  * **Contextual NPC Dialogue:** NPCs deliver unique messages based on the current scene, ranging from ominous welcomes to angry confrontations.

  * **Branching Endings:** The game features multiple narrative conclusions, including a **Good Ending** 🌟, **Bad Ending** 💀, and **Game Over** 💔, influenced by player performance and choices.

  * **Dynamic Music:** Background music can **fade out** 🎶 and new tracks (e.g., boss music) **fade in** 🎵 during key narrative moments or boss encounters.

  * **Camera Effects:** The main camera's background color **randomly changes** 🌈 at set intervals, adding a dynamic visual element.

  * **Fullscreen Fade:** Utilizes a **sprite-based fade-to-black effect** ⚫ for smooth scene transitions, including a delay before loading the Bad Ending scene.

-----

## 🛠️ Technical Details

  * **Engine:** Unity (2D Template)
  * **Programming Language:** C\#
  * **UI:** TextMeshPro for advanced text rendering.
  * **Physics:** Unity's 2D Physics engine (Rigidbodies, Colliders, Raycasting).
  * **Persistence:** PlayerPrefs for saving and loading game data.

### Core Scripts 📜

| Script Name | Description |
| :---------- | :---------- |
| **PlayerController** 🎮 | Manages all player input, movement physics, sprite changes, and interactions with enemies and collectibles. |
| **EnemyController** 👾 | Provides a foundation for diverse enemy AI, including ground-based patrols and airborne seeking behaviors. |
| **NPCController** 🗣️ | Handles contextual dialogue, animation, and in-game events triggered by the player's presence, including a dramatic transformation into a boss enemy. |
| **LevelTracker** 📊 | A central hub for all game state data, including score, lives, and level progression, which it saves and loads between scenes. |
| **CutsceneManager** 🎬 | Facilitates text-based storytelling with dynamic cutscene selection, typewriter effects, and smooth transitions to gameplay. |
| **StartMenu** ▶️ | Manages the main menu screen, handling button presses and keyboard input to start the game or access special levels. |
| **CameraSystem** 📸 | Ensures the camera smoothly follows the player character while remaining within the defined boundaries of each level. |
| **RandomColorManager** 🎨 | A simple script that adds a dynamic visual element by randomly changing the camera's background color at regular intervals. |

-----

## 🚀 How to Play

  * **Movement:** Use **Arrow Keys** or **A/D** (Keyboard) to move Blocky left/right.
  * **Jump:** Press **Spacebar** to jump.
  * **Sprint:** Hold **Left Shift** for a burst of speed.
  * **Rotate:** Use **Up/Down Arrow Keys** to rotate Blocky.

-----

## 🗓️ Project Timeline

  * **Start Date:** April 18, 2024
  * **Art:** April 18 - April 27, 2024
  * **Level Design:** April 28 - May 5, 2024
  * **Coding & Implementation:** May 6 - May 10, 2024
  * **Testing & Refinement:** May 11 - May 12, 2024
  * **Completion Date:** May 12, 2024

-----

## ⬇️ Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/your-username/BlockysEscape.git
    ```
2.  **Open in Unity:** Open the cloned project in Unity Editor (tested with Unity 2023.x or later).
3.  **Build Settings:** Ensure all game scenes are added to `File > Build Settings` in the correct order for progression.
4.  **Run:** Open the `StartMenu` scene (usually found in `Assets/Scenes/StartMenu.unity`) and press the Play button in the Unity Editor.
