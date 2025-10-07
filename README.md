--------------------------------------------------------

# Task 2: [Pattern Name] Implementation

## Student Info
- Name: Mark Stoegbauer
- ID: 01279456

## Pattern: Singleton
### Implementation
The GameManager singleton pattern was used to keep track of and reset all states of the game between entering and exiting the scene. It uses a public enum to keep track of game states across the scene and also pulls and updates all UI each time it has to restart the scene. Its organized so that all related functions are next to each other and hard to miss the references used. 

### Game Integration
The GameManager gets all important components each time it's started and makes sure they are in working order. It uses another Singleton of an AudioManager to handle playing sounds as well. Something else the GameManager does alongside the rest of the scene is it connects to and keeps track of the boss and displays its health for the player. It also pauses the game when needed and makes sure to write debug logs into the console for development. Many other GameObjects in the scene can call functions from GameManager (like for the boss hp tracker) or to tell it that an enemy has been killed.

## Game Description
- Title: Solo Space Squares
- Controls: Movement - W,A,S,D | Look - Mouse Cursor | Shoot - Left Click
- Objective: Gain enough points by killing enemies to summon and defeat the boss.

## Repository Stats
- Total Commits: 14
- Development Time: 22 hrs

--------------------------------------------------------
