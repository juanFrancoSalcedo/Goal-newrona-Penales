# Interactivo penales Newrona - Guía del Cliente

## Tabla de Contenidos

1. [Qué es Interactivo penales Newrona](#1-qué-es-goal-newrona)
2. [Cómo se Juega](#2-cómo-se-juega)
3. [Flujo de Pantallas](#3-flujo-de-pantallas)
4. [Formulario de Registro](#4-formulario-de-registro)
5. [Panel de Administración](#5-panel-de-administración)
6. [Datos de los Jugadores](#6-datos-de-los-jugadores)
7. [Personalización](#7-personalización)
8. [Requerimientos de Hardware](#8-requerimientos-de-hardware)
9. [Montaje de la Experiencia](#9-montaje-de-la-experiencia)
10. [Consejos Importantes](#10-consejos-importantes)
11. [Solución de Problemas](#11-solución-de-problemas)
12. [Preguntas Frecuentes](#12-preguntas-frecuentes)
13. [Contacto y Soporte](#13-contacto-y-soporte)

---

## 1. Qué es Interactivo penales Newrona

Interactivo penales Newrona es una experiencia interactiva de activación de marca donde los participantes compiten en un juego de tiros al arco, lanzando un balón real hacia una pantalla gigante que simula la portería.

**Características principales:**

- **Experiencia inmersiva**: Juego de tiros al arco donde lanzas un balón físico hacia una pantalla gigante que actúa como portería
- **Competencia en vivo**: Ranking en tiempo real de los mejores jugadores
- **Captura de datos**: Registro automático de información de contacto de los participantes
- **Personalizable**: Imágenes, colores y configuración adaptable a tu marca
- **Fácil de operar**: Panel de administración intuitivo para controlar la experiencia

Ideal para eventos, activaciones de marca, ferias, y cualquier contexto donde quieras captar la atención del público y generar engagement.

---

## 2. Cómo se Juega

### Interacción

El juego simula un tiro al arco usando un **balón físico** que el jugador lanza hacia una **pantalla gigante**. Cuando el balón impacta la pantalla, se registra la posición del impacto como un disparo al arco.

**Objetivo**: Anotar la mayor cantidad de goles posibles en 5 intentos.

**Mecánica**:
1. El jugador ve el arco con dianas de puntuación en la pantalla
2. El jugador lanza el balón hacia la pantalla para disparar
3. El impacto del balón en la pantalla simula el tiro al arco
4. La puntuación depende de dónde impacte el tiro (más cerca del centro = más puntos)
5. La dificultad aumenta progresivamente con cada tiro exitoso
6. Al completar los 5 intentos, se muestra el resultado y el ranking

### Sistema de Puntuación

- **Diana central**: Mayor puntuación (zona de precisión)
- **Diana intermedia**: Puntuación media
- **Diana exterior**: Puntuación baja
- **Fuera del arco**: Sin puntos

**Dificultad progresiva**: Con cada tiro exitoso, la diana se hace más pequeña y se mueve, aumentando el desafío.

---

## 3. Flujo de Pantallas

El juego sigue este flujo de navegación:

```
┌─────────────────┐
│ 1. Pantalla de  │  Pantalla inicial con branding
│    Inicio       │  Botón "Comenzar"
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ 2. Instrucciones│  Cómo jugar
│                 │  Reglas del juego
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ 3. Formulario   │  Registro del jugador
│                 │  Nombre, correo, teléfono
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ 4. Gameplay     │  Juego principal
│                 │  5 intentos
│                 │  Cuenta regresiva entre tiros
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ 5. Resultado y  │  Puntuación final
│    Ranking      │  Top 5 jugadores
│                 │  Posición del jugador actual
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ 6. Admin        │  Panel de administración
│                 │  (Acceso restringido)
└─────────────────┘
```

### Descripción de cada pantalla

| # | Pantalla | Descripción |
|---|---|---|
| 1 | **Inicio** | Pantalla de bienvenida con el branding del evento. El jugador presiona "Comenzar" para iniciar. |
| 2 | **Instrucciones** | Explicación visual de cómo jugar. Se puede avanzar con un botón o automáticamente. |
| 3 | **Formulario** | El jugador ingresa sus datos: nombre, correo electrónico y teléfono. |
| 4 | **Gameplay** | El juego principal. Cuenta regresiva inicial, 5 intentos de tiro lanzando el balón a la pantalla, timer de juego. |
| 5 | **Resultado** | Muestra la puntuación del jugador y el ranking de los 5 mejores jugadores. |
| 6 | **Admin** | Panel de administración para configurar la experiencia (requiere contraseña). |

---

## 4. Formulario de Registro

El formulario captura la información de contacto de los participantes.

### Campos requeridos

| Campo | Validación | Ejemplo |
|---|---|---|
| **Nombre y Apellido** | Obligatorio | Juan Pérez |
| **Correo Electrónico** | Debe contener @ y un punto (.) | juan.perez@empresa.com |
| **Teléfono** | Entre 10 y 12 dígitos | 5551234567 |

### Validaciones automáticas

- **Teléfono**: Solo acepta números, mínimo 10 dígitos, máximo 12 dígitos
- **Correo**: Debe tener formato válido (contener @ y al menos un punto después del @)
- **Nombre**: Campo obligatorio, no puede estar vacío

### Qué hacer si un jugador no puede completar el formulario

- Verificar que el teléfono tenga entre 10 y 12 dígitos (solo números)
- Verificar que el correo tenga el formato correcto (ejemplo@dominio.com)
- Si el problema persiste, el jugador puede contactar al operador del evento

---

## 5. Panel de Administración

El panel de administración permite configurar la experiencia sin necesidad de conocimientos técnicos.

### Acceso al Panel

| Campo | Detalle |
|---|---|
| **Contraseña** | `NWG` |
| **Cómo acceder** | Desde la pantalla de inicio, buscar el acceso al panel admin |

### Funcionalidades del Panel

#### 5.1 Configurar Tiempo de Cuenta Regresiva

**Qué es**: El tiempo de espera entre cada tiro del jugador.

**Cómo cambiarlo**:
1. Acceder al panel de administración
2. Buscar el campo "Duración del Countdown"
3. Ingresar el nuevo valor en segundos (ejemplo: 5 para 5 segundos)
4. El cambio se aplica automáticamente

**Valor recomendado**: 3-5 segundos

#### 5.2 Cambiar Imágenes

**Qué es**: Puedes personalizar las imágenes que aparecen en la experiencia (logos, fondos, elementos de marca).

**Cómo cambiarlas**:
1. Acceder al panel de administración
2. Presionar el botón "Logo" o "LogoUI"
3. Elegir el archivo PNG desde tu computadora
4. La imagen se actualiza automáticamente en la experiencia

**Formato soportado**: Solo archivos `.png`

**Ajustar tamaño**:
- Usar el slider "Escala de Imagen" para ajustar el tamaño
- Rango: de 50% a 200% del tamaño original

#### 5.3 Cambiar Color del Portero

**Qué es**: Personaliza el color del portero para que combine con tu marca.

**Cómo cambiarlo**:
1. Acceder al panel de administración
2. Presionar el botón del color deseado
3. El color del portero cambia automáticamente

#### 5.4 Abrir Carpeta de Datos

**Qué es**: Accede rápidamente a la carpeta donde se guardan los datos de los jugadores.

**Cómo usarlo**:
1. Acceder al panel de administración
2. Presionar el botón "Data"
3. Se abre el explorador de Windows en la ubicación de los archivos CSV

---

## 6. Datos de los Jugadores

### Qué se captura

Cada jugador que completa el formulario y juega genera un registro con:

| Dato | Descripción |
|---|---|
| **ID único** | Identificador automático (no modificar) |
| **Nombre** | Nombre completo del jugador |
| **Correo** | Email del jugador |
| **Teléfono** | Número de teléfono |
| **Puntuación** | Score total obtenido en el juego |
| **Tiempo** | Tiempo que tardó en completar el juego |

### Formato de almacenamiento

Los datos se guardan en archivos **CSV** (formato compatible con Excel, Google Sheets, etc.).

**Nombre del archivo**: `goal-AAAA-MM-DD.csv` (ejemplo: `goal-2026-06-08.csv`)

**Ubicación**: Carpeta de datos del aplicativo (accesible desde el panel admin)

### Cómo exportar los datos

1. Acceder al panel de administración
2. Presionar el botón "Data"
3. Copiar los archivos CSV de la fecha del evento
4. Abrirlos con Excel, Google Sheets, o cualquier herramienta de análisis

### Estructura del CSV

```csv
uid,nombre,correo,telefono,score,tiempo
abc-123,Juan Perez,juan@empresa.com,5551234567,150,45.3
def-456,Maria Lopez,maria@empresa.com,5557654321,200,38.7
```

**Nota**: Los jugadores se ordenan automáticamente por puntuación (mayor a menor).

---

## 7. Personalización

### Qué puedes personalizar

| Elemento | Descripción | Cómo |
|---|---|---|
| **Imágenes** | Logos, fondos, elementos de marca | Panel Admin → Seleccionar Imagen |
| **Colores del portero** | Color del uniforme del portero | Panel Admin → Botones de color |
| **Tiempo de cuenta regresiva** | Duración entre tiros | Panel Admin → Input de duración |
| **Escala de imágenes** | Tamaño de las imágenes cargadas | Panel Admin → Slider de escala |

### Personalización avanzada

Para cambios más profundos (sonidos, dificultad, textos, elementos 3D), contactar al equipo de desarrollo.

---

## 8. Requerimientos de Hardware

### Requerimientos recomendados

| Componente | Especificación |
|---|---|
| **PC** | Procesador Intel Core i3-10100 o superior / AMD Ryzen 3 3100 o superior |
| **RAM** | 8 GB mínimo / 16 GB recomendado |
| **Almacenamiento** | SSD de 256 GB mínimo (para carga rápida del juego) |
| **Gráficos** | Gráficos integrados Intel UHD 630+ o GPU dedicada básica (NVIDIA GT 1030 / GTX 1050) |
| **Pantalla** | Resolución 1200x800 o superior, conexión HDMI o DisplayPort |
| **Balón** | Balón físico para lanzar hacia la pantalla |
| **Sistema operativo** | Windows 10/11 64-bit |
| **Audio** | Parlantes activos con conexión de 3.5mm o USB |
| **Conexión** | No requiere internet para jugar (solo para actualizar datos) |

### Notas importantes

- **No se requiere una PC gamer**: El juego es liviano y funciona bien con hardware básico de oficina.
- **SSD recomendado**: Mejora significativamente el tiempo de carga inicial del juego.
- **Conexión estable**: Asegurar que la pantalla esté bien conectada y configurada en la resolución correcta antes del evento.

---

## 9. Montaje de la Experiencia

> **[PENDIENTE]** - Esta sección será completada con el equipo técnico.

### Pasos básicos (placeholder)

1. **Instalación física**: Colocar la pantalla en la ubicación del evento y demarcar la zona de lanzamiento del balón
2. **Conexión de PC**: Conectar el PC a la pantalla
3. **Configuración de audio**: Conectar parlantes si es necesario
4. **Lanzar el aplicativo**: Ejecutar el archivo `.exe` del juego
5. **Prueba inicial**: Verificar que todo funcione correctamente
6. **Configuración final**: Ajustar imágenes y parámetros desde el panel admin

---

## 10. Consejos Importantes

### Mantenimiento del aplicativo

- **No mover los archivos**: Mantener todos los archivos del juego en su carpeta original. Mover o eliminar archivos puede causar errores.
- **No renombrar archivos**: El juego depende de nombres de archivo específicos.
- **No instalar actualizaciones automáticas**: Desactivar actualizaciones de Windows durante el evento para evitar reinicios inesperados.

### Durante el evento

- **Revisar periódicamente**: Verificar que el juego esté funcionando correctamente cada cierto tiempo.
- **Monitorear el espacio en disco**: Los archivos CSV de datos pueden crecer si el evento es muy largo.
- **Tener un backup**: Llevar una copia de seguridad del aplicativo en un USB.

### Después del evento

- **Exportar los datos**: Copiar los archivos CSV antes de apagar el equipo.
- **Apagar correctamente**: Cerrar el juego antes de apagar el PC.

---

## 11. Solución de Problemas

> **[PENDIENTE]** - Esta sección será completada con problemas comunes específicos del proyecto.

### Problemas comunes (placeholder)

| Problema | Solución |
|---|---|
| **El juego no inicia** | Verificar que todos los archivos estén en su carpeta original. Reiniciar el PC. |
| **La pantalla no registra impactos** | Verificar la conexión de la pantalla. Reiniciar el PC. |
| **No se guardan los datos** | Verificar que haya espacio en disco. Revisar permisos de escritura en la carpeta de datos. |
| **Las imágenes no se actualizan** | Verificar que el archivo sea PNG. Reiniciar el juego después de cambiar la imagen. |
| **El audio no se escucha** | Verificar la conexión de los parlantes. Revisar el volumen del sistema. |

---

## 12. Preguntas Frecuentes

> **[PENDIENTE]** - Esta sección será completada con preguntas reales de los clientes.

### Preguntas generales (placeholder)

**P: ¿Cuántos jugadores pueden jugar al mismo tiempo?**
R: El juego está diseñado para un jugador a la vez.

**P: ¿Se necesita internet para jugar?**
R: [Por definir]

**P: ¿Cuánto dura una partida?**
R: Depende del tiempo configurado y la velocidad del jugador, pero generalmente entre 1-3 minutos.

**P: ¿Puedo cambiar el idioma del juego?**
R: [Por definir - consultar con el equipo de desarrollo]

**P: ¿Qué pasa si un jugador no completa el formulario?**
R: El jugador no puede avanzar al juego. El formulario es obligatorio.

**P: ¿Puedo ver los resultados en tiempo real?**
R: Sí, el ranking se actualiza después de cada partida.

---

## 13. Contacto y Soporte

> **[PENDIENTE]** - Esta sección será completada con la información de contacto real.

### Soporte técnico

| Canal | Contacto |
|---|---|
| **Email** | [Por definir] |
| **Teléfono** | [Por definir] |
| **Horario de atención** | [Por definir] |

### Documentación adicional

- [Documento Técnico](./TechnicalDocument.md) - Para desarrolladores y equipo técnico

---

*Guía del Cliente - Interactivo penales Newrona - Newrona Brand Activation*
