# Interactivo penales Newrona - Documento Tecnico

## Tabla de Contenidos

1. [Resumen del Proyecto](#1-resumen-del-proyecto)
2. [Informacion General](#2-informacion-general)
3. [Arquitectura del Sistema](#3-arquitectura-del-sistema)
4. [Mapa de Navegacion y Flujo del Usuario](#4-mapa-de-navegacion-y-flujo-del-usuario)
5. [Mecanicas de Juego](#5-mecanicas-de-juego)
6. [Sistema de Puntuacion](#6-sistema-de-puntuacion)
7. [Sistema de Datos y Almacenamiento](#7-sistema-de-datos-y-almacenamiento)
8. [Panel de Administracion](#8-panel-de-administracion)
9. [Sistema de Audio](#9-sistema-de-audio)
10. [Estructura del Proyecto](#10-estructura-del-proyecto)
11. [Dependencias y Plugins](#11-dependencias-y-plugins)
12. [Referencia de Scripts](#12-referencia-de-scripts)
13. [Configuracion y Personalizacion](#13-configuracion-y-personalizacion)

---

## 1. Resumen del Proyecto

**Interactivo penales Newrona** es una experiencia interactiva de activacion de marca desarrollada en Unity 6. El proyecto consiste en un juego de tiros al arco donde los participantes compiten por la mayor puntuacion en un formato de kiosk/touchscreen para eventos presenciales.

Los jugadores se enfrentan a un arco virtual con dianas de puntuacion. Tienen un numero limitado de intentos dentro de un tiempo determinado. Al finalizar, sus datos y puntuacion se registran y se muestra un ranking de los mejores jugadores.

---

## 2. Informacion General

| Campo | Detalle |
|---|---|
| **Motor** | Unity 6000.3.6f1 |
| **Plataforma objetivo** | Windows Standalone (kiosk/touchscreen) |
| **Tipo de proyecto** | Activacion de marca / Evento de marketing |
| **Interaccion** | Balon fisico lanzado hacia pantalla |
| **Lenguaje** | C# |
| **Escena principal** | `Assets/Level/Scenes/Game.unity` |
| **Fuente** | Montserrat (TextMesh Pro) |
| **Render Pipeline** | URP (Universal Render Pipeline) |

### 2.1 Requerimientos de Hardware Recomendados

| Componente | Especificación | Notas |
|---|---|---|
| **CPU** | Intel Core i3-10100 / AMD Ryzen 3 3100 | Generación 10ma+ recomendada |
| **RAM** | 8 GB mínimo / 16 GB recomendado | DDR4 2666MHz+ |
| **GPU** | Intel UHD 630+ / NVIDIA GT 1030 / GTX 1050 | Gráficos integrados modernos son suficientes |
| **Almacenamiento** | SSD 256 GB+ | Mejora tiempo de carga |
| **Resolución** | 1200x800 (nativa del proyecto) | Escalable a 1080p si es necesario |
| **Sistema Operativo** | Windows 10/11 64-bit | Actualizado con últimos drivers |
| **Puertos** | HDMI o DisplayPort para pantalla | USB para audio y periféricos |

**Nota**: El proyecto está optimizado para ser liviano. No requiere hardware de gama alta debido a la resolución moderada, escena 3D simple y post-procesamiento mínimo.

---

## 3. Arquitectura del Sistema

### 3.1 Patron de Diseno Principal

El proyecto utiliza una arquitectura basada en **eventos** con el patron **Mediator** para la comunicacion entre sistemas desacoplados.

```
┌─────────────────────────────────────────────────────┐
│                  GameStateContext                     │
│         (Maquina de Estados del Juego)               │
│                                                      │
│   GameStateMediator  ←→  GameEventType (enum)        │
│   ScoreMediator      ←→  ScoreEventType (enum)       │
│   ShotMediator       ←→  ShotEventType (enum)        │
└─────────────────────────────────────────────────────┘
```

### 3.2 Mediadores

| Mediador | Tipo de Dato | Descripcion |
|---|---|---|
| `GameStateMediator` | `UnityEvent` (sin datos) | Eventos de estado del juego |
| `ScoreMediator` | `UnityEvent<int>` | Eventos de puntuacion (transporta el valor del score) |
| `ShotMediator` | `UnityEvent<TypeShot>` | Eventos de tiro (transporta el tipo: Goal, Wrong, None) |

### 3.3 Patrones Utilizados

- **Singleton**: `ScoreManager`, `EndGameManager`, `RankingManager`, `RaycastManager`, `ManagerAudio`, `AdminManager`, `FileSelectorService`
- **Mediator**: `GameStateMediator`, `ScoreMediator`, `ShotMediator`
- **State Machine**: `GameStateContext` con estados definidos en `GameEventType`
- **Observer**: Suscripciones a eventos via `OnEnable`/`OnDisable`
- **Strategy**: `IScoreReceptor` para diferentes tipos de receptores de puntuacion
- **Interface-based**: `IAdminListener` para componentes que reaccionan a cambios del admin

### 3.4 Libreria Base (B_Extensions)

Libreria interna de utilidades ubicada en `Assets/ExternalAssets/B_Extension/`:

| Clase | Ubicacion | Descripcion |
|---|---|---|
| `Singleton<T>` | `Base/Singleton.cs` | Patron singleton generico |
| `BaseButtonAttendant` | `Base/BaseButtonAttendant.cs` | Clase base para botones con helpers |
| `Timer` | `Simple/Timer.cs` | Componente de temporizador |
| `KeyStorage` | `Base/KeyStorage.cs` | Constantes para PlayerPrefs |
| `BaseDoAnimationController` | `Anima DG Control/` | Controlador de animaciones DOTween |
| `FormController` | `Advance/Form/` | Controlador de formularios |

---

## 4. Mapa de Navegacion y Flujo del Usuario

### 4.1 Estados del Juego (GameEventType)

```
┌───────────┐
│  Idle /   │  Estado inicial (pantalla de inicio)
│  Tutorial │
└─────┬─────┘
      │ Jugador presiona "Start" (click o tecla Enter)
      ▼
┌───────────────┐
│ IntroCountDown│  Cuenta regresiva de introduccion
│               │  (duracion configurable, default 5s)
│               │  Se activa sonido de publico
└───────┬───────┘
        │ Espera igual a la duracion del countdown
        ▼
┌──────────────┐     ┌──────────────────────────┐
│ GameStarted  │◄───►│  Ciclo de Juego           │
│              │     │  1. Jugador hace click    │
│  Timer activo│     │  2. Raycast detecta impacto│
│              │     │  3. Se calcula puntuacion  │
│              │     │  4. Countdown entre tiros  │
│              │     │  5. Se repite              │
└──────┬───────┘     └──────────────────────────┘
       │
       │ 3 intentos completados O tiempo agotado
       ▼
┌───────────────┐
│ GameFinished  │  Se detiene el timer
│               │  Suena silbatazo final
│               │  Se muestra ranking (top 5)
└───────┬───────┘
        │ Jugador completa formulario
        ▼
┌───────────────┐
│ FormSubmitted │  Se guardan datos del jugador
│               │  Se resetean intentos
│               │  Se actualiza ranking
└───────────────┘
```

### 4.2 Flujo Detallado del Jugador

1. **Pantalla de Inicio**: El jugador ve la pantalla principal con un boton de inicio.
2. **Inicio del Juego**: Al presionar "Start" (click o tecla Enter), se activa la cuenta regresiva de introduccion.
3. **Cuenta Regresiva**: Se muestra un countdown visual con animacion de escala (punch). El sonido de publico comienza.
4. **Fase de Juego**:
   - El timer general del juego comienza a contar.
   - El jugador hace click en la pantalla para disparar al arco.
   - El sistema de raycast detecta donde impacto el tiro.
   - Se calcula la puntuacion segun la distancia al centro de la diana.
   - Se muestra una animacion del portero.
   - Se pausa el timer y se muestra un countdown entre tiros (configurable).
   - El ciclo se repite hasta completar 3 intentos o que el tiempo se agote.
5. **Fin del Juego**: Se muestra el ranking con los 5 mejores jugadores.
6. **Formulario**: El jugador ingresa sus datos (nombre, correo, telefono).
7. **Registro**: Los datos y la puntuacion se guardan en un archivo CSV.
8. **Ranking Actualizado**: Se muestra el ranking actualizado con el jugador actual resaltado.

---

## 5. Mecanicas de Juego

### 5.1 Sistema de Disparo

- **Input**: Click izquierdo del mouse (via `ShotManager` → `RaycastManager`).
- **Deteccion**: Raycast fisico desde la camara principal hacia la posicion del mouse.
- **Layer**: Solo detecta objetos en la capa `receptorLayer` configurada en `RaycastManager`.
- **Condicion**: Solo registra tiros cuando el estado es `GameStarted`.
- **Bloqueo**: El input se bloquea durante el countdown entre tiros (`RaycastManager.Locked`).

### 5.2 Tipos de Tiro (TypeShot)

| Valor | Descripcion |
|---|---|
| `None` | Sin tiro |
| `Goal` | Tiro valido que impacto en una diana o receptor con puntuacion positiva |
| `Wrong` | Tiro que impacto en un receptor con puntuacion cero o negativa |

### 5.3 Sistema de Intentos

- **Maximo de intentos**: 3 (configurable en `EndGameManager.maxAttempts`).
- Cada tiro valido incrementa el contador de intentos.
- Cuando se alcanzan los 3 intentos, el juego termina (`GameFinished`).
- Si el timer general se agota antes, se fuerzan los intentos al maximo (`SetfullAttempts()`).

### 5.4 Dificultad Progresiva

La diana principal (`Diana`) aumenta su dificultad con cada tiro exitoso:

| Nivel (dianaLevel) | Efecto | Multiplicador |
|---|---|---|
| 0 | Estado inicial | x1 |
| 1 | La diana se reduce al 70% de su tamano original. Los rangos de puntuacion tambien se reducen al 70%. | x1 |
| 2 | La diana se mueve hacia arriba. | x2 |
| 3 | La diana se mueve en un ciclo continuo (arriba/abajo). | x3 |
| 4 | 50% de probabilidad de que la diana desaparezca (segun posicion izquierda/derecha). Se mueve hacia arriba. | x3 |
| 5 | Sin cambios adicionales. | x3 |

### 5.5 Temporizadores

| Temporizador | Descripcion | Configurable |
|---|---|---|
| **Game Timer** | Timer principal del juego. Cuenta el tiempo total de juego. | No (desde Inspector) |
| **Countdown Timer** | Cuenta regresiva entre tiros. Se muestra visualmente con animacion. | Si (via Admin Panel o PlayerPrefs) |
| **Intro Countdown** | Cuenta regresiva inicial antes de que comience el juego. Usa la misma duracion del Countdown Timer. | Si (misma configuracion) |

---

## 6. Sistema de Puntuacion

### 6.1 Diana (Diana.cs)

La diana es el objetivo principal. La puntuacion se calcula segun la **distancia al centro** de la diana:

```csharp
float distanceToCenter = Vector2.Distance(hitPoint, dianaPosition);
```

Cada diana tiene un array de `ScoreRange`:

| Campo | Tipo | Descripcion |
|---|---|---|
| `minDistance` | `float` | Distancia minima desde el centro |
| `maxDistance` | `float` | Distancia maxima desde el centro |
| `score` | `int` | Puntuacion otorgada en ese rango |

El score final se multiplica por el `multiplier` actual (segun el nivel de dificultad).

### 6.2 ScoreReceptor (ScoreReceptor.cs)

Receptores simples con un valor de puntuacion fijo. Si el score es <= 0, el tiro se clasifica como `Wrong`; si es positivo, como `Goal`.

### 6.3 Flujo de Puntuacion

```
Click del jugador
    → ShotManager.CastRayFromMouse()
    → RaycastManager.CastRay()
    → IScoreReceptor.ApplyScore(hitPoint, TypeShot)
    → ScoreMediator.Publish(ScoreApplied, score)
    → ScoreManager.OnScoreApplied() → acumula TotalScore
    → ScoreMediator.Publish(TotalScoreChanged, TotalScore)
    → ShotMediator.Publish(ShotApplied, TypeShot)
    → EndGameManager.UpdateScore() → incrementa intentos
```

### 6.4 Visualizacion del Score

| Componente | Funcion |
|---|---|
| `ScoreFullText` | Muestra el score total con formato configurable (ej: "Score: {0}"). Animacion de punch scale. |
| `ScoreCardCreator` | Instancia tarjetas flotantes (+N) con cada tiro. Se destruyen automaticamente tras fade out. |
| `ScoreCard` | Tarjeta individual con animacion de aparicion y desvanecimiento. |
| `ShotStateIcon` | Muestra iconos segun el tipo de tiro (Goal/Wrong). |
| `TextAttemps` | Muestra el numero de intentos actuales. |

---

## 7. Sistema de Datos y Almacenamiento

### 7.1 Datos del Jugador (PlayerData)

| Campo | Tipo | Descripcion |
|---|---|---|
| `uid` | `string` | Identificador unico (GUID generado automaticamente) |
| `nombre` | `string` | Nombre del jugador |
| `correo` | `string` | Correo electronico |
| `telefono` | `string` | Numero de telefono |
| `score` | `int` | Puntuacion total obtenida |
| `tiempo` | `float` | Tiempo de juego registrado |

### 7.2 Almacenamiento CSV

Los datos se almacenan en archivos CSV con el formato:

```
Nombre: goal-YYYY-MM-DD.csv
Ubicacion: Application.persistentDataPath
Codificacion: UTF-8
```

**Estructura del CSV:**
```csv
uid,nombre,correo,telefono,score,tiempo
abc-123,Juan Perez,juan@email.com,555-1234,150,45.3
def-456,Maria Lopez,maria@email.com,555-5678,200,38.7
```

**Caracteristicas:**
- Se crea un archivo nuevo por dia.
- Los campos con comas, comillas o saltos de linea se escapan correctamente.
- El encabezado se escribe solo si el archivo no existe.
- Los jugadores se ordenan por score descendente al cargar.

### 7.3 Ranking

- Se muestran los **5 mejores jugadores** (`MaxRankingPositions = 5`).
- Los datos se cargan desde el CSV cada vez que se actualiza el ranking.
- El jugador actual se resalta en una tarjeta especial (`currentPlayerCard`).
- Cada posicion se muestra con: numero de posicion, nombre y puntuacion (formato "{0} pts").

### 7.4 PlayerPrefs (Configuracion Persistente)

| Key | Tipo | Descripcion |
|---|---|---|
| `KEY_GOALKEEPER_COLOR` | `string` | Color del portero en formato HTML RGBA |
| `KEY_COUNTDOWN_DURATION` | `float` | Duracion del countdown entre tiros |
| `KEY_IMAGE_SCALE` | `float` | Escala de la imagen de textura cargada |

---

## 8. Panel de Administracion

### 8.1 Acceso

| Campo | Detalle |
|---|---|
| **Contrasena** | `NWG` |
| **Validacion** | Case-insensitive (se convierte a mayusculas) |
| **Ubicacion del codigo** | `AdminPassword.cs` |

### 8.2 Funcionalidades del Panel Admin

| Funcionalidad | Script | Descripcion |
|---|---|---|
| **Duracion del Countdown** | `CountdownTimerSetter.cs` | Input field para configurar la duracion (en segundos) del countdown entre tiros. Se guarda en PlayerPrefs. |
| **Seleccion de Imagen** | `ButtonSelectFile.cs` + `FileSelectorService.cs` | Abre un dialogo de seleccion de archivos (.png). Copia la imagen a `StreamingAssets/texture.png`. |
| **Escala de Imagen** | `ImageScaleSlider.cs` | Slider para ajustar la escala de la imagen cargada (rango configurable, default 0.5 - 2.0). |
| **Color del Portero** | `ButtonColorChanger.cs` + `MaterialColorChanger.cs` | Botones predefinidos para cambiar el color del portero. Se guarda en PlayerPrefs. |
| **Abrir Carpeta de Datos** | `ButtonOpenFolder.cs` | Abre el explorador de Windows en la carpeta `Application.persistentDataPath` donde se guardan los CSV. |

### 8.3 Sistema de Notificaciones Admin

El `AdminManager` notifica a todos los componentes registrados como `IAdminListener` cuando se carga un nuevo archivo. Los componentes que implementan esta interfaz:

- `CountdownTimer` - Actualiza su duracion
- `UIImageLoader` - Recarga la imagen
- `TextureLoader` - Recarga la textura
- `MaterialColorChanger` - Reaplica el color

---

## 9. Sistema de Audio

El `ManagerAudio` gestiona todos los sonidos del juego:

| Tipo de Sonido | Trigger | Descripcion |
|---|---|---|
| **Crowds (Publico)** | `IntroCountDown` | Sonido ambiente de publico en loop continuo. Se reproduce aleatoriamente sin repeticiones consecutivas. |
| **Kick (Patada)** | Al disparar | Sonido aleatorio de patada al golpear el balon. |
| **Start Claps** | Inicio del juego | Aplausos de inicio. |
| **Whistle Start** | Inicio del juego | Silbatazo de inicio. |
| **Whistle Random** | Aleatorio | Silbato aleatorio. |
| **Whistle End** | `GameFinished` | Silbatazo de fin de juego. |
| **Cheers (Aplausos)** | Gol/acierto | Celebracion aleatoria. |
| **Select UI** | Interaccion UI | Sonido de seleccion en interfaz. |

---

## 10. Estructura del Proyecto

```
Interactivo penales Newrona/
├── Assets/
│   ├── Animations/              # Animaciones del proyecto
│   ├── Art/                     # Recursos graficos
│   │   └── 2D/
│   │       └── Font/
│   │           └── Montserrat/  # Fuente principal
│   ├── Code/                    # Codigo fuente del proyecto
│   │   ├── Scripts/
│   │   │   ├── Features/        # Logica de caracteristicas del juego
│   │   │   │   ├── Score/       # Sistema de puntuacion
│   │   │   │   └── Shots/       # Sistema de disparos
│   │   │   ├── Services/        # Servicios (datos, raycast, archivos)
│   │   │   ├── Utils/           # Utilidades (carga de texturas, colores)
│   │   │   └── FileBrowser/     # File browser multiplataforma
│   │   ├── Shaders/             # Shaders personalizados
│   │   └── SO/                  # Scriptable Objects
│   ├── ExternalAssets/          # Assets externos y librerias
│   │   ├── B_Extension/         # Libreria interna de utilidades
│   │   │   ├── Base/            # Singleton, BaseButtonAttendant, KeyStorage
│   │   │   ├── Simple/          # Timer, ScreenAdapter, TypingAnimation
│   │   │   └── Advance/         # Form, SceneLoader, HierarchyEnabler
│   │   ├── SuperGoalie/         # Asset de porteria 3D (arco, portero, estadio)
│   │   ├── Anima DG Control/    # Controladores de animacion DOTween
│   │   └── Fantasy Skybox/      # Skybox del entorno
│   ├── Level/                   # Contenido de nivel
│   │   ├── Scenes/
│   │   │   └── Game.unity       # Escena principal del juego
│   │   └── Prefabs/             # Prefabs del juego
│   │       ├── AudioManager.prefab
│   │       ├── ButtonOpenData.prefab
│   │       ├── CardRanking.prefab
│   │       ├── ScoreCard.prefab
│   │       └── ScreenForm.prefab
│   ├── Plugins/                 # Plugins de terceros
│   │   └── Demigiant/           # DOTween
│   ├── Resources/               # Recursos de Unity
│   ├── Scenes/                  # Escenas adicionales
│   │   └── SampleScene.unity
│   ├── Settings/                # Configuraciones de Unity
│   ├── StreamingAssets/         # Assets de streaming (imagenes cargables)
│   │   ├── texture.png
│   │   └── texture2.png
│   ├── TextMesh Pro/            # TextMesh Pro
│   ├── ChromaKey.shader         # Shader para chroma key
│   ├── VideoTribunaChroma.mp4   # Video de tribuna con chroma key
│   └── Video.RT.renderTexture   # Render texture para video
├── Packages/                    # Paquetes de Unity
├── ProjectSettings/             # Configuracion del proyecto
└── Build/                       # Builds del proyecto
```

---

## 11. Dependencias y Plugins

| Plugin/Package | Version/Ubicacion | Uso |
|---|---|---|
| **DOTween** (Demigiant) | `Assets/Plugins/Demigiant/` | Animaciones de UI (punch scale, fade, movimiento) |
| **TextMesh Pro** | Built-in Unity | Renderizado de texto en UI y 3D |
| **B_Extensions** | `Assets/ExternalAssets/B_Extension/` | Libreria interna: Singleton, botones, timer, formularios, key storage |
| **SuperGoalie** | `Assets/ExternalAssets/SuperGoalie/` | Asset 3D: arco, portero, estadio, sonidos de futbol |
| **Anima DG Control** | `Assets/ExternalAssets/Anima DG Control/` | Controladores de animacion basados en DOTween |
| **Fantasy Skybox** | `Assets/ExternalAssets/Fantasy Skybox/` | Cielo/entorno del escenario |
| **StandaloneFileBrowser** | `Assets/Code/Scripts/FileBrowser/` | Dialogo de seleccion de archivos multiplataforma |
| **Input System** | Unity (new) | Input del mouse para disparos |

---

## 12. Referencia de Scripts

### 12.1 Features (Caracteristicas)

| Script | Namespace | Descripcion |
|---|---|---|
| `ButtonStartGame` | - | Boton de inicio. Cambia estado a `IntroCountDown` y luego a `GameStarted`. Soporta tecla Enter. |
| `ButtonEndResult` | - | Boton de resultado final. Soporta tecla Enter en estado `GameFinished`. |
| `ButtonSubmitForm` | - | Boton de envio de formulario. Cambia estado a `FormSubmitted`. |
| `ButtonSelectFile` | `Features` | Abre el navegador de archivos para seleccionar una imagen PNG. |
| `ButtonOpenFolder` | `Features` | Abre el explorador de Windows en una ruta configurable. |
| `ButtonColorChanger` | `Features` | Cambia el color del portero y lo guarda en PlayerPrefs. |
| `CountDownHandler` | - | Maneja el countdown entre tiros. Bloquea el input y pausa el timer. |
| `CountdownTimer` | `Features` | Componente de cuenta regresiva con animacion de texto. Configurable via admin. |
| `CountdownTimerSetter` | `Features` | Input field para configurar la duracion del countdown desde el admin. |
| `EndGameManager` | `Features` | Gestiona los intentos del jugador y el fin del juego. |
| `TimerGameEventHandler` | - | Conecta el timer del juego con el EndGameManager. |
| `TimerText` | `Features` | Muestra el tiempo del timer en un texto TMP. |
| `TextAttemps` | - | Muestra el numero de intentos actuales en un texto TMP. |
| `AdminPassword` | - | Panel de acceso con contrasena para el admin. |
| `AdminManager` | `Features` | Notifica a todos los IAdminListener cuando se carga un archivo. |
| `GameStateMediator` | - | Mediador de eventos de estado del juego (Subscribe/Publish). |
| `IconFootSubmit` | - | Muestra/oculta icono de pie segun disponibilidad del boton submit. |
| `ShotStateIcon` | `Features` | Muestra iconos segun el tipo de tiro (Goal/Wrong). |
| `ImageScaleSlider` | `Features` | Slider para ajustar la escala de la imagen cargada. |
| `ScreenRankingManager` | - | Activa el canvas de fin de juego cuando el juego termina. |
| `ManagerAudio` | - | Gestor de audio del juego. Reproduce sonidos segun eventos. |

### 12.2 Score (Puntuacion)

| Script | Descripcion |
|---|---|
| `ScoreManager` | Singleton que acumula la puntuacion total. |
| `ScoreMediator` | Mediador de eventos de puntuacion. |
| `ShotMediator` | Mediador de eventos de tiro. |
| `Diana` | Diana principal con puntuacion por distancia y dificultad progresiva. |
| `ScoreReceptor` | Receptor de puntuacion simple con valor fijo. |
| `ScoreRange` | Struct con rango de distancia y puntuacion. |
| `ScoreFullText` | Texto que muestra el score total con animacion. |
| `ScoreCard` | Tarjeta flotante que muestra "+N" con fade out. |
| `ScoreCardCreator` | Instancia ScoreCards al recibir eventos de puntuacion. |
| `CardRanking` | Tarjeta de ranking (posicion, nombre, score). |
| `RankingManager` | Gestiona el ranking top 5 y la persistencia CSV. |
| `CrowdHandler` | Activa animacion de publico al cambiar el score. |
| `ShotStateIcon` | Iconos visuales segun tipo de tiro. |
| `IScoreReceptor` | Interfaz para receptores de puntuacion. |
| `TypeShot` | Enum: None, Goal, Wrong. |

### 12.3 Services (Servicios)

| Script | Descripcion |
|---|---|
| `RaycastManager` | Singleton que gestiona los raycasts desde el mouse a la escena. |
| `CsvPlayerSaver` | Guarda y lee datos de jugadores en archivos CSV. |
| `PlayerData` | Modelo de datos del jugador (IComparable por score descendente). |
| `CountdownDataService` | Persiste la duracion del countdown en PlayerPrefs. |
| `FileSelectorService` | Abre dialogo de archivos y copia a StreamingAssets. |
| `IAdminListener` | Interfaz para componentes que reaccionan a cambios admin. |

### 12.4 Utils (Utilidades)

| Script | Descripcion |
|---|---|
| `UIImageLoader` | Carga imagenes PNG desde StreamingAssets a un componente Image UI. |
| `TextureLoader` | Carga imagenes PNG desde StreamingAssets a materiales 3D. |
| `MaterialColorChanger` | Cambia el color de un material usando MaterialPropertyBlock. |

---

## 13. Configuracion y Personalizacion

### 13.1 Parametros Configurables desde el Inspector

| Componente | Parametro | Descripcion |
|---|---|---|
| `EndGameManager` | `maxAttempts` | Maximo numero de intentos (default: 3) |
| `CountdownTimer` | `_duration` | Duracion del countdown (default: 3s, sobreescribible por admin) |
| `CountdownTimer` | `_zeroPlaceholder` | Texto cuando llega a cero (default: "Start") |
| `CountdownTimer` | `_punchScale` | Intensidad de animacion de escala (default: 0.3) |
| `Diana` | `scoreRanges` | Array de rangos de puntuacion por distancia |
| `Diana` | `pathDuration` | Duracion de animaciones de movimiento |
| `Diana` | `isLeft` | Si la diana esta en el lado izquierdo (afecta comportamiento nivel 4) |
| `RaycastManager` | `rayDistance` | Distancia maxima del raycast (default: 100) |
| `RaycastManager` | `receptorLayer` | Layer mask para detectar receptores |
| `ScoreCard` | `displayDuration` | Duracion de visualizacion de la tarjeta (default: 1.5s) |
| `ScoreCard` | `fadeDuration` | Duracion del fade out (default: 0.5s) |
| `ImageScaleSlider` | `minScale` / `maxScale` | Rango de escala de imagen (default: 0.5 - 2.0) |
| `ScoreFullText` | `format` | Formato del texto de score (default: "Score: {0}") |
| `TextAttemps` | `format` | Formato del texto de intentos (default: "Attempts: {0}") |

### 13.2 Archivos de Texturas

Las imagenes se cargan desde `Assets/StreamingAssets/`:

| Archivo | Uso |
|---|---|
| `texture.png` | Imagen principal cargable via admin |
| `texture2.png` | Imagen secundaria |

El admin puede reemplazar `texture.png` seleccionando cualquier archivo PNG desde el panel de administracion.

### 13.4 Atajos de Teclado

| Tecla | Accion | Condicion |
|---|---|---|
| `Enter` | Iniciar juego | Estado inicial |
| `Enter` | Confirmar resultado final | Estado `GameFinished` |

---

*Documento generado para el proyecto Interactivo penales Newrona - Newrona Brand Activation*
