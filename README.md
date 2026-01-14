# SmarTest AI

Aplicație web pentru generarea și evaluarea automată a întrebărilor din domeniul Inteligenței Artificiale. Sistemul suportă multiple tipuri de întrebări (strategie, CSP, Minimax, Nash Equilibrium) și oferă evaluare inteligentă a răspunsurilor folosind procesare de limbaj natural.

---

## Cuprins

1. [Cerințe de Sistem](#cerințe-de-sistem)
2. [Instalare](#instalare)
3. [Configurare Baza de Date](#configurare-baza-de-date)
4. [Rulare](#rulare)
5. [Structura Proiectului](#structura-proiectului)
6. [Funcționalități](#funcționalități)
7. [API Endpoints](#api-endpoints)
8. [Tehnologii Utilizate](#tehnologii-utilizate)

---

## Cerințe de Sistem

### Software necesar

- Python 3.10 sau superior
- Node.js 18.x sau superior
- PostgreSQL 14 sau superior
- pip (package manager Python)
- npm (package manager Node.js)

### Sistem de operare

Aplicația a fost testată pe:
- Ubuntu 22.04 LTS (WSL2)
- Windows 11

---

## Instalare

### 1. Clonare repository

```bash
git clone https://github.com/callmenoob77/Proiect-AI.git
cd Proiect-AI
```

### 2. Backend (Python/FastAPI)

```bash
# Crearea mediului virtual
python -m venv venv

# Activare mediu virtual
# Linux/macOS:
source venv/bin/activate
# Windows:
.\venv\Scripts\activate

# Instalare dependențe
pip install -r requirements.txt
```

Dependențele principale instalate:
- `fastapi` - Framework web asincron
- `uvicorn` - Server ASGI
- `sqlalchemy` - ORM pentru baza de date
- `sentence-transformers` - Model NLP pentru evaluare semantică

### 3. Frontend (React)

```bash
cd frontend
npm install
cd ..
```

---

## Configurare Baza de Date

### 1. Creare bază de date PostgreSQL

```sql
CREATE DATABASE smartest_ai;
```

### 2. Configurare conexiune

Editați fișierul `app/database.py` și actualizați URL-ul de conexiune:

```python
DATABASE_URL = "postgresql://username:password@localhost:5432/smartest_ai"
```

### 3. Adăugare tipuri de întrebări în baza de date

Executați următoarele comenzi în PostgreSQL (pgAdmin sau psql):

```sql
-- Tipuri de probleme de bază
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'N_QUEENS';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'HANOI';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'GRAPH_COLORING';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'KNIGHT_TOUR';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'BACKTRACKING_ASSIGNMENT';

-- Tipuri adăugate ulterior
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'RIVER_CROSSING';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'WATER_JUG';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'SLIDING_PUZZLE';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'X_O';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'CSP_PROBLEM';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'A_STAR_DESCRIPTION';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'MINIMAX_TREE';
ALTER TYPE question_type ADD VALUE IF NOT EXISTS 'GAME_MATRIX';
```

---

## Rulare

Aplicația necesită două terminale separate.

### Terminal 1 - Backend

```bash
cd Proiect-AI
source venv/bin/activate  # sau .\venv\Scripts\activate pe Windows
uvicorn app.main:app --reload
```

Backend-ul va porni pe `http://localhost:8000`

### Terminal 2 - Frontend

```bash
cd Proiect-AI/frontend
npm start
```

Frontend-ul va porni pe `http://localhost:3000`

### Verificare funcționare

Accesați în browser:
- Frontend: http://localhost:3000
- API Documentation (Swagger): http://localhost:8000/docs

---

## Structura Proiectului

```
Proiect-AI/
├── app/                          # Backend FastAPI
│   ├── core/                     # Logica de bază
│   │   ├── generator.py          # Generator întrebări principale
│   │   ├── evaluator.py          # Evaluator răspunsuri (NLP)
│   │   ├── csp_generator.py      # Generator CSP
│   │   ├── minimax_generator.py  # Generator Minimax
│   │   ├── minimax_solver.py     # Solver Alpha-Beta
│   │   ├── nash_generator.py     # Generator Nash Equilibrium
│   │   └── csp_solver.py         # Solver CSP
│   ├── models/                   # Modele SQLAlchemy
│   ├── routers/                  # API endpoints
│   │   ├── generator_api.py      # Endpoint generare întrebări
│   │   ├── answer_api.py         # Endpoint evaluare răspunsuri
│   │   ├── test_api.py           # Endpoint mod test
│   │   └── custom_question_api.py # Endpoint întrebări personalizate
│   ├── schemas/                  # Scheme Pydantic
│   ├── database.py               # Configurare DB
│   └── main.py                   # Entry point aplicație
├── frontend/                     # Frontend React
│   ├── src/
│   │   ├── components/
│   │   │   ├── TreeVisualizer.js       # Vizualizare arbori Minimax
│   │   │   ├── GameMatrixVisualizer.js # Vizualizare matrice Nash
│   │   │   └── TestMode.jsx            # Componenta mod test
│   │   └── App.js                # Componenta principală
│   └── package.json
├── requirements.txt              # Dependențe Python
└── README.md                     # Acest fișier
```

---

## Funcționalități

### Tipuri de Întrebări

| Tip | Descriere | Mod răspuns |
|-----|-----------|-------------|
| Strategii Algoritmice | N-Queens, Turnurile Hanoi, Graph Coloring | Multiple choice / Text |
| CSP | Forward Checking, MRV, Arc Consistency | Multiple choice / Text |
| Minimax | Arbori de joc cu Alpha-Beta Pruning | Multiple choice / Text |
| Nash Equilibrium | Jocuri în formă normală | Multiple choice / Text |

### Moduri de Utilizare

1. **Întrebare Generată** - Sistem generează aleatoriu o întrebare
2. **Întrebare pe Pattern** - Utilizatorul specifică parametrii (problemă, instanță)
3. **Mod Test** - Set de întrebări cu scor final

### Evaluare Răspunsuri

Sistemul folosește o abordare hibridă pentru evaluarea răspunsurilor text:
- 60% - Similaritate semantică (Sentence Transformers)
- 40% - Potrivire cuvinte cheie

Model NLP utilizat: `paraphrase-multilingual-MiniLM-L12-v2` (suport limba română)

---

## API Endpoints

### Generare Întrebări

```
POST /api/generate/strategy
Content-Type: application/json

{
  "answer_type": "multiple" | "text",
  "difficulty": 1 | 2 | 3,
  "chapter_filter": "Strategii algoritmice" | "Algoritmi de cautare si CSP" | "Teoria Jocurilor"
}
```

### Evaluare Răspuns

```
POST /api/answer/submit
Content-Type: application/json

{
  "question_id": 123,
  "user_answer": "Răspunsul utilizatorului"
}
```

### Generare Test

```
POST /api/test/generate?num_questions=5&difficulty=2
```

### Trimitere Test

```
POST /api/test/submit
Content-Type: application/json

{
  "1": "Răspuns întrebare 1",
  "2": "Răspuns întrebare 2"
}
```

Documentație completă API disponibilă la: http://localhost:8000/docs

---

## Tehnologii Utilizate

### Backend
- Python 3.12
- FastAPI
- SQLAlchemy
- PostgreSQL
- Sentence Transformers (NLP)

### Frontend
- React 18
- Tailwind CSS
- Lucide React (iconuri)

---

## Autori

Proiect realizat pentru cursul de Inteligență Artificială.

---

## Notă

La prima rulare, modelul Sentence Transformers va fi descărcat automat (aproximativ 400MB). Aceasta poate dura câteva minute în funcție de viteza conexiunii la internet.
