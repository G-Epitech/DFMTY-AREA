# 🎨 Graphic library

First, we made the choice to compare only 2D libraries because of the requirements and expectations of the project. The R-Type is originally a 2D game. Also, we did not choose the **Advanced Gameplay** track, so a 3D game was not our goal. Therefore, a 2D library was a good and sufficient way.

# Libraries **benchmark**

We choose 3 popular 2D libraries to compare

| Criterion | **SFML** | **SDL** | **Raylib** |
| --- | --- | --- | --- |
| **C++ API** | ✅ | ❌ | ❌ |
| **Abstraction level** | High level: easy API for sound, textures, etc. | Low level: needs often many lines to perform an action | High level: very simple for common tasks |
| **Ease of learning** | Easy due to OOP pattern and we already used it | Requires higher comprehension of some mechanisms what can make usage more complex | Designed to be easy to learn and use |
| **OS Compatibility** | Windows, macOS, Linux | Windows, macOS, Linux, Android, iOS | Windows, macOS, Linux, Raspberry Pi, WebAssembly |
| **Community & Documentation** | Good documentation and active community | Very large community and extensive documentation (thanks to its longevity) | Smaller but growing community, excellent beginner-friendly documentation |
| **Features** | 2D graphics, audio, networking, event handling, basic OpenGL support | 2D/3D graphics, audio, event handling, excellent multiplatform support | 2D/3D graphics, audio, input handling, shaders, VR support, designed for fast and simple games |
| **Used before** | Yes | Little bit | No |

# Final choice

According to comparative table above, we choose to use the **SFML** library that requires less C++ wrapping work and with we’ve already worked.

If we had used the SDL we would have to wrap it and write more complex code, so it was not what we wanted, and if we had gone with the Raylib, we would have had to learn it from scratch and wrap it as well, which could have been a long process.

So regarding these observations, we can say that the SFML is more adapted to our project:

- **No C++ wrapping needed**, so more time to really develop project features
- **We already used it**, so we don’t need to learn how to use it
- **Good cross-platform compatibility**