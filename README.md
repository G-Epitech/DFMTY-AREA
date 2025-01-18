# 🔆 AREA
The ultimate automation platform

- [📦 Epitech repository](https://github.com/EpitechPromo2027/B-DEV-500-NAN-5-2-area-matheo.coquet)
- [📄 PDF Project](./docs/subject.pdf)

---

## 🚀 How to Run the Project

### Development Version
For the development version, you do not need to build the mobile application as it takes a significant amount of time. Simply run:

```sh
docker-compose up --build
```

Or, if the images are already built:

```sh
docker-compose up
```

### Production Version
In the production environment, you must build the mobile application. To include the mobile build, set the `BUILD_MOBILE` environment variable before running the commands:

#### Windows PowerShell
```powershell
$env:BUILD_MOBILE="true"
docker-compose up --build
```

#### Linux/macOS
```sh
export BUILD_MOBILE=true
docker-compose up --build
```

---

Made by [Suceveanu Dragos](https://github.com/sdragos1), [Flavien Chenu](https://github.com/flavien-chenu), [TekMath](https://github.com/tekmath), [Thomaltarix](https://github.com/Thomaltarix) & [Yann Masson](https://github.com/Yann-Masson)
