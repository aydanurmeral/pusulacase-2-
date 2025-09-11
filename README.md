# StudentAutomation

Kısa rehber: frontend (Vue.js) ve backend (.NET + PostgreSQL) içerir.

---

##  Ortam değişkenlerini ayarlayın

- Backend için `appsettings.json` yerine `appsettings.example.json` kullanabilirsiniz.
- Docker ile PostgreSQL kullanıyorsanız environment değişkenlerini ayarlayın:

POSTGRES_USER=your_db_user
POSTGRES_PASSWORD=your_db_password
POSTGRES_DB=your_db_name

---

##  PostgreSQL çalıştırın

Docker Compose ile:

```bash
docker compose up -d
Container çalıştığından emin olun.
3️⃣ Backend’i çalıştırın
cd backend/StudentAutomation.Api
dotnet run
API default olarak http://localhost:5166 üzerinde çalışır.
4️⃣ Frontend’i çalıştırın
cd frontend
npm install      # ilk defa çalıştırıyorsanız
npm run dev
Frontend default olarak http://localhost:5173 üzerinde çalışır.
