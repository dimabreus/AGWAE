# AGWAE (A Game Without An Engine)

AGWAE is a basic game framework written in C# that implements core game mechanics and physics from scratch, with a focus on simplicity and manual control over the functionality.

This framework provides essential game features such as scene management, physics, and rendering, without relying on third-party game engines or libraries (except for SFML for window rendering).

## Features

- **Scene Management**: System for managing multiple scenes.
- **Time Management**: `Time.DeltaTime` and fixed updates for consistent frame rates.
- **Layer Management**: Manage different layers for objects in the game.
- **Camera System**: Camera that follows a specific object.
- **GameObject**: Base object with position, rotation, scale, sprite, and other properties.
- **UIObject**: A `GameObject` with click actions.
- **ColliderObject**: `GameObject` with a collider, made of vertices and a debug visualization.
- **RigidbodyObject**: A `ColliderObject` with physics properties for collisions and movement.
- **Tilemap**: System for organizing game objects in a grid for easier placement.

## Physics System

- **Shape**: A collection of vertices and related methods for defining object boundaries.
- **Projection**: A method for projecting shapes along an axis, used in collision detection.
- **SAT (Separating Axis Theorem)**: Main class for detecting collisions and calculating the Minimum Translation Vector (MTV).

## Example

Two simple scenes have been added to demonstrate the framework:
- **SMainMenu**: Main menu of the game.
- **SGameScene**: Example platformer game scene with basic objects and physics.

## Notes

This framework is intended for developers who want to create their own game mechanics and features without relying on pre-built solutions from larger game engines. It is designed to be minimal and flexible, giving you full control over game logic, physics, and rendering.

Currently, there is no visual editor—everything is done through code.

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/dimabreus/AGWAE.git
   ```

2. Open the project in your preferred C# IDE (e.g., Visual Studio).

3. Install **SFML** (Simple and Fast Multimedia Library) for window rendering:
   - Download SFML.Net from [here](https://www.sfml-dev.org/).
   - Follow installation instructions for your platform.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
