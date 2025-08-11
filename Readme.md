# Giny - A Dofus Server Emulator

Giny is a comprehensive and modular server emulator for the popular MMORPG, Dofus, written entirely in C# .NET. It provides a robust foundation for recreating the server-side logic of the game, allowing developers to host their own Dofus worlds. The emulator is designed with a clear separation of concerns, broken down into several distinct projects that work together to deliver a complete and extensible Dofus experience.

## Components

Giny's architecture is divided into the following key components:

### Core & I/O

*   **Giny.Core**: This foundational project contains a collection of C# utilities that are not directly related to Dofus but provide essential functionalities. These include robust systems for serialization and cryptography, as well as networking tools that form the backbone of the server's communication.

*   **Giny.IO**: This component serves as a serialization wrapper specifically designed to handle Dofus's proprietary client files. It allows the server to read and interpret the various data files used by the Dofus client, a crucial step in accurately recreating the game world.

### Database & Protocol

*   **Giny.ORM**: A custom-built Object-Relational Mapper (ORM) is included to manage all interactions with the server's database. This tailored ORM is designed to handle the specific data structures and relationships found within the Dofus game.

*   **Giny.Protocol**: The communication between the Dofus client and server is facilitated by a specific network protocol. Giny.Protocol is an auto-generated library that precisely mirrors the protocol used by the Dofus client sources. This ensures seamless communication and compatibility between the client and the Giny emulator. It is utilized by both the authentication and world servers.

### Game Servers

*   **Giny.World**: This is the heart of the emulator, containing the core logic for the Dofus world server. It manages everything from player movement and interaction to NPC behavior, combat mechanics, and all other gameplay-related features.

*   **Giny.Auth**: The authentication server is a separate component responsible for handling player logins and authentication requests. It validates user credentials and, upon successful authentication, directs the player to the world server.

### Development Tools

Giny also includes a suite of powerful tools to aid in the development and customization of your Dofus server:

*   **Giny.MapEditor**: A work-in-progress implementation of a Dofus map editor. This tool is intended to allow for the creation and modification of in-game maps.

*   **Giny.SpellTree**: For developers and server administrators interested in the intricate details of character abilities, Giny.SpellTree provides a visual representation of spell effects. Written in WPF, this tool helps in understanding and customizing the game's complex spell system.

*   **Giny.WorldEditor**: Built with MudBlazor, this tool provides a user-friendly interface for editing both `.d2o` game files and the server's database content. This allows for easy modification of items, monsters, quests, and other game data.

*   **Giny.ProtocolBuilder**: This essential tool automates the process of generating the Dofus network protocol from the client's source files. This ensures that the emulator can stay up-to-date with any changes introduced in new versions of the Dofus client.

*   **Giny.DatabaseSynchronizer**: To create an accurate and functional game world, the server's database must be populated with data from the Dofus client. This tool is used to automatically generate the server database from the client's local database files.

ClickUp : https://app.clickup.com/9003026450/v/s/90030432232

Client link : https://mega.nz/file/9QkVmAZT#-0HA6YtFf7PGJQ8N-qtJCmRdafpMZPU7TkxbbF3RXaM
