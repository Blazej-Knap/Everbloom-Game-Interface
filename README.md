# 🌸 Everbloom – Game Interface

> Projekt zaliczeniowy: Interfejs użytkownika do gry w stylu farming sim / cozy RPG, zbudowany w **Unity (URP)** z użyciem systemu UI Toolkit i wbudowanego Unity UI (uGUI).

---

## 📋 Opis projektu

**Everbloom** to interfejs użytkownika do gry farmerskiej tworzonej na potrzeby zajęć z tworzenia UI. Projekt obejmuje kompletny zestaw ekranów i mechanizmów nawigacji między nimi, w tym:

- Menu główne z przyciskami akcji
- Ekran nowej gry z walidacją formularza
- Ekran ustawień z trwałym zapisem preferencji (PlayerPrefs)
- Scenę rozgrywki z kamerą podążającą za graczem, paskiem narzędzi oraz systemem interakcji z obiektami
- Ekran kalendarza oraz podsumowania dnia
- System dialogów z NPC (Luna)
- System ekwipunku

---

## 🗂️ Struktura projektu

```
Everbloom-Game-Interface/
├── Everbloom/              # Projekt Unity
│   └── Assets/
│       ├── Scenes/         # Sceny gry
│       ├── Scripts/        # Logika C#
│       │   └── UI/         # Skrypty interfejsu użytkownika
│       ├── Prefabs/        # Prefabrykaty
│       ├── Sprites/        # Grafiki i ikony
│       ├── UI/             # Pliki UI (obrazy tła, panele)
│       └── Resources/      # Zasoby ładowane dynamicznie
└── Execute/                # Skompilowana wersja gry (.exe)
    └── Everbloom.exe
```

---

## 🎮 Sceny

| Scena | Opis |
|---|---|
| `MainMenu` | Menu główne z opcjami: Nowa gra, Ustawienia, Wyjście |
| `NewGameMenu` | Formularz nazwy farmy, walidacja wejścia |
| `SettingsMenu` | Ustawienia dźwięku (muzyka, efekty, głośność) |
| `GameScene` | Główna scena rozgrywki z mapą, graczem i interakcjami |
| `Calendar` | Widok kalendarza w grze |
| `DaySummary` | Podsumowanie dnia po zakończeniu rozgrywki |

---

## 🧩 Skrypty

### Główne (`Assets/Scripts/`)

| Skrypt | Opis |
|---|---|
| `GlobalInputManager.cs` | Singleton trwający przez całą grę. Obsługuje globalne wejście (np. `Escape` → wyjście z gry). |
| `InteractionBridge.cs` | Most między kliknięciem obiektu a akcją (Calendar, DaySummary, Dialogue). Obsługuje kliknięcia myszy i klawisz `E`. |
| `InteractionTrigger.cs` | Trigger wyzwalający interakcję po wejściu gracza w strefę. |
| `SimpleCameraFollow.cs` | Płynne podążanie kamery za celem z interpolacją (Lerp). |

### UI (`Assets/Scripts/UI/`)

| Skrypt | Opis |
|---|---|
| `MainMenuManager.cs` | Zarządza panelami i przyciskami menu głównego. |
| `NewGameManager.cs` | Obsługuje formularz nowej gry, waliduje nazwę farmy przed przejściem do `GameScene`. |
| `SettingsManager.cs` | Wczytuje i zapisuje ustawienia dźwięku przez `PlayerPrefs`. |
| `GlobalUIFixer.cs` | Automatycznie naprawia EventSystem, wyłącza raycasting na tekstach, dodaje `ButtonHoverEffect` do wszystkich przycisków. |
| `ButtonHoverEffect.cs` | Efekty wizualne przycisku: powiększenie przy najechaniu, zmiana koloru, animacja wciśnięcia. |
| `SpriteHoverEffect.cs` | Analogiczny efekt hover dla obiektów Sprite (nie UI). |
| `DialogueController.cs` | Pokazuje/ukrywa okno dialogowe z NPC; zamykane dowolnym klawiszem. |
| `DialogueToggle.cs` | Przełącznik otwierający dialog z poziomu UI. |
| `InventoryToggle.cs` | Obsługuje widoczność panelu ekwipunku. |
| `ToolbarSelectionController.cs` | Pasek szybkiego dostępu (hotbar); zaznaczanie slotów klawiszami `1`–`0`. |
| `SceneController.cs` | Ładuje sceny Calendar i DaySummary. |
| `SceneLoader.cs` | Pomocnicze ładowanie scen po nazwie (wywoływane z przycisków). |
| `ReturnToGame.cs` | Powrót do `GameScene` z ekranów podrzędnych. |

---

## ⚙️ Wymagania

- **Unity** 2022.3 LTS lub nowszy (projekt korzysta z **Universal Render Pipeline – URP**)
- **Unity Input System** (nowy system wejścia)
- **TextMeshPro** (zainstalowane przez Package Manager)
- Pakiety asset store (dołączone w projekcie):
  - `Fantasy Wooden GUI Free`
  - `Inventory System – Kinnly`
  - `Pixel Cursors`
  - `Pixel Skies DEMO`
  - `TopDown 2D RPG BE3`

---

## 🚀 Uruchomienie

### ▶️ Wersja skompilowana (gotowy .exe)

1. Przejdź do folderu `Execute/`
2. Uruchom `Everbloom.exe`

> ⚠️ Folder `Execute/` musi być uruchamiany w całości (nie przenoś samego `.exe` bez pozostałych plików).

### 🛠️ Wersja edytorska (Unity)

1. Otwórz Unity Hub
2. Wybierz **Add project from disk** i wskaż folder `Everbloom/`
3. Otwórz projekt w Unity (wymagane Unity 2022.3+)
4. W **Build Settings** upewnij się, że sceny są dodane w kolejności:
   - `MainMenu` (index 0)
   - `NewGameMenu`
   - `SettingsMenu`
   - `GameScene`
   - `Calendar`
   - `DaySummary`
5. Uruchom scenę `MainMenu` klawiszem **Play**

---

## 🎨 Funkcje UI

- **Efekty hover na przyciskach** – powiększenie + zmiana koloru + animacja wciśnięcia (zaimplementowane przez `ButtonHoverEffect` i `GlobalUIFixer`)
- **Walidacja formularza** – ekran nowej gry wymaga podania nazwy farmy przed uruchomieniem gry
- **Trwałe ustawienia** – preferencje dźwięku zapisywane przez `PlayerPrefs` między sesjami
- **Pasek narzędzi** – możliwość wyboru slotu klawiszami `1`–`0`
- **System dialogów** – rozmowa z NPC Luna, zamykana dowolnym klawiszem
- **Interakcja z obiektami** – klawisz `E` lub kliknięcie myszą otwiera powiązane ekrany
- **Kamera** – płynna kamera podążająca za postacią gracza

---

## 📁 Dodatkowe informacje

- Projekt nie używa OneDrive do przechowywania kompilacji — buduj do lokalnego katalogu poza `OneDrive`, aby uniknąć problemów z synchronizacją podczas budowania.
- Singleton `GlobalInputManager` jest dodany do sceny `MainMenu` i trwa przez cały czas życia gry (`DontDestroyOnLoad`).
- `GlobalUIFixer` powinien być umieszczony w każdej scenie lub zostać połączony z `GlobalInputManager` jako trwały obiekt.

---

## 👤 Autor

Projekt tworzony na potrzeby zajęć akademickich z projektowania interfejsów użytkownika.
