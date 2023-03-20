# Welcome to the ScriptableObjects Paddle Ball project
This demo project showcases design patterns and game architecture using ScriptableObjects. It’s intended to be explored together with the e-book, *Create modular game architecture in Unity with ScriptableObjects*. 

The demo provides examples of how ScriptableObjects can help you create components that are testable and scalable, while still being designer-friendly. Although such a game could be built with far fewer lines of code, the purpose here is to show ScriptableObjects in action in a popular game design. 

## Getting Started
Start the project from the **Bootloader_Scene** scene. There are two parts to the project:

1. The code examples of **design patterns** that show how ScriptableObjects can solve recurring problems in Unity development 
2. The **mini games** that show how to put these patterns together into small working applications

## Design Patterns
The design patterns using ScriptableObjects that are included in the example project are: 

* **Data containers:** How to store static data for use at runtime
* **Delegate objects:** How to encapsulate interchangeable logic into its own ScriptableObject
* **Event Channels:** How to help your objects communicate
* **Enums:** How to use a ScriptableObject instead of a traditional enum for better comparison operations
* **Runtime Sets:** How to store frequently accessed objects or components from any scene without the need for a singleton

## Mini games
The project includes several variants of a basic paddle and ball game to illustrate how to put these patterns together into functional game systems:

* **Classic:** Shows a basic implementation of ScriptableObjects in a classic paddle and ball game
* **Hockey:** Shows how to define custom wall positions using serialized text
* **Foosball:** Shows how to use a Prefab definition for the level walls  

## Learn more about ScriptableObjects
The implementations in this project are provided for inspiration and education. In most cases, the code examples favor clarity over compactness.

You will likely need additional tools and support to adapt these patterns to your specific production needs. For additional examples of what is possible with ScriptableObjects, please see the following:

* [“Overthrowing the MonoBehaviour Tyranny in a Glorious ScriptableObject Revolution”](https://www.youtube.com/watch?v=6vmRwLYWNRo)
* [“Game Architecture with ScriptableObjects”](https://www.youtube.com/watch?v=raQ3iHhE_Kk)

## Learn more advanced coding best practices in Unity
The ScriptableObjects e-book is the third in our series of advanced guides for Unity developers. Each guide, authored by experienced programmers, provides best practices for specific topics that are important to development teams. 

[Create a C# style guide: Write cleaner code that scales](https://blog.unity.com/engine-platform/clean-up-your-code-how-to-create-your-own-c-code-style) guides teams in developing a style guide to help unify their approach for creating a more cohesive codebase.
 
[Level up your code with game programming patterns](https://blog.unity.com/games/level-up-your-code-with-game-programming-patterns) highlights best practices for using the SOLID principles and common programming patterns to create scalable game code architecture in your Unity project.
