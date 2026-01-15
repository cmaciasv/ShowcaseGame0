# ShowcaseGame0 (Temporary Name)

A high-polish, "Neon Void" themed arcade brick-breaker built in Unity for WebGL. This project serves as a showcase for professional game architecture, satisfying Juice-driven gameplay, and optimized performance.

## 🚀 Vision
Deliver a sensory-rich arcade experience where every collision feels impactful. The game balances retro "Breakout" mechanics with modern "synesthetic" feedback (Audio + Visual synchronization).

### Key Features
- **Neon Void Aesthetics:** High-contrast, bloom-heavy visuals using URP.
- **Juice system:** Centralized feedback pipeline for screenshake, hitstop, and chromatic aberration.
- **Professional Architecture:** Built with **Zenject (Extenject)** for dependency injection and loose coupling.
- **Performance Optimized:** Target <50MB WebGL build with stable 60fps on standard browser hardware.

## 🛠 Tech Stack
- **Engine:** Unity (Latest LTS)
- **Render Pipeline:** Universal Render Pipeline (URP) 2D
- **Dependency Injection:** Zenject / Extenject
- **Events/Signals:** Zenject SignalBus
- **Physics:** Unity 2D Physics
- **Input:** New Unity Input System (Cross-platform support)

## 📁 Project Structure
The project follows a split structure between the Unity project and the BMAD metadata:
```
ShowcaseGame0/
├── unityproject/      # The actual Unity project
│   ├── Assets/        # Game assets and scripts
│   ├── .gitignore     # Unity-specific gitignore
│   └── ...
├── _bmad/             # Agent metadata and workflows
├── _bmad-output/      # Planning and design artifacts
└── README.md
```

### Unity Project Structure
Inside `unityproject/Assets/_Game/`:
```
Assets/_Game/
├── _Config/      # ScriptableObjects and Game Settings
├── Art/          # Shaders, Textures, and VFX
├── Audio/        # SFX and Procedural Audio
├── Prefabs/      # Game Entities and Systems
├── Scenes/       # Boot and Gameplay
└── Scripts/      # C# Source
    ├── Core/     # State Machines, Game Loops
    ├── Data/     # Persistence and JSON Helpers
    ├── Entities/ # Player, Ball, Bricks
    └── Systems/  # Managers and Installers
```

## ⚙️ Setup & Development
1. **Clone the repository.**
2. **Open the `unityproject` folder with Unity Hub** (Target: WebGL).
3. **Import Zenject/Extenject** from the Asset Store.
4. **Ensure IL2CPP stripping** is handled via the included `unityproject/Assets/link.xml`.

## 📈 Roadmap
- [x] Architecture Planning
- [/] Project Setup & Zenject Integration
- [ ] Core Gameplay Loop (Paddle & Ball)
- [ ] Juice injection (Post-processing & Feedback)
- [ ] Level Progression & Bricks
- [ ] Polish & WebGL Deployment

---
*Created with the help of BMAD (Big Model Agile Development) agents.*
