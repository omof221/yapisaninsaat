# Yapısan İnşaat - Yönetim Paneli ve Kurumsal Web Sitesi

Bu proje, bir inşaat firmasının dijital vitrinini yönetmek için geliştirilmiş, yüksek performanslı ve dinamik bir web çözümüdür. Modern bir kurumsal önyüz ile bu önyüzü saniyeler içinde güncelleyebileceğiniz kapsamlı bir yönetim panelini bir araya getirir.

---

## Öne Çıkan Özellikler

###  Kurumsal Önyüz (Web Sitesi)
* **Dinamik Proje Sergisi:** Tamamlanan ve devam eden projelerin detaylı görsellerle sunumu.
* **Foto Galeri:** Firmanın işçilik kalitesini gösteren yüksek çözünürlüklü fotoğraf sergisi.
* **Müşteri Yorumları (Testimonials):** Güven veren, dinamik olarak yönetilebilen referans geri bildirimleri.
* **İnteraktif SSS:** Müşterilerin en çok sorduğu soruların düzenli bir şekilde listelenmesi.
* **Modern Hero Slider:** Ana sayfada dikkat çekici kampanya veya duyuru görselleri.
* **Hızlı İletişim:** CTA (Call to Action) bölümleri ve dinamik iletişim bilgileri.

###  Yönetim Paneli (CMS)
* **Tam Denetim:** Sitedeki tüm metinleri, görselleri ve ayarları teknik bilgi gerekmeden güncelleme imkanı.
* **Gelişmiş Görsel Yönetimi:** Proje resimlerini, referans logolarını ve slider görsellerini kolayca yükleme ve silme.
* **Pop-up Yönetimi:** Önemli duyurular veya kampanyalar için dinamik pop-up mesajları oluşturma.
* **Ekip & Referans Yönetimi:** Şirket çalışanlarını ve iş ortaklarını kolayca ekleyip çıkarma.
* **Esnek Yapı:** `IsActive` (Aktif/Pasif) ve `Order` (Sıralama) özellikleri ile içeriklerin sitedeki yerini anlık değiştirme.

---

##  Teknoloji Yığını
* **Backend:** .NET 6, Entity Framework Core (Code-First)
* **Frontend:** ASP.NET Core MVC (Razor Views), ViewComponents
* **Veritabanı:** SQL Server
* **Dosya Yönetimi:** Yerel dosya sistemi (`wwwroot/uploads`) üzerinden akıllı görsel yükleme.

---

## Önemli Dosyalar ve Klasör Yapısı
* **`Program.cs`:** Uygulama yapılandırması ve Routing ayarları.
* **`Models/AppDbContext.cs`:** Veritabanı şeması ve DbSet tanımlamaları.
* **`Helpers/FileHelper.cs`:** Görsel yükleme, format kontrolü ve silme mantığı.
* **`Views/Shared/Components/`:** Önyüzü oluşturan dinamik bileşenler (Slider, Referanslar vb.).

---

##  Yerel Kurulum (Adım Adım)

1.  **Depoyu Klonlayın:**
    ```bash
    git clone <repo-url>
    cd yapisaninsaat/yapisaninsaat
    ```

2.  **Bağımlılıkları Yükleyin:**
    ```bash
    dotnet restore
    ```

3.  **Veritabanı Bağlantısını Yapılandırın:**
    `appsettings.json` dosyasında bağlantı dizesini kendinize göre düzenleyin:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=.\\SQLEXPRESS;Database=YapiInsaat;Integrated Security=true;TrustServerCertificate=True;"
      }
    }
    ```

4.  **Veritabanını Güncelleyin:**
    ```bash
    dotnet ef database update
    ```

5.  **Görsel Klasörlerini Hazırlayın:**
    `wwwroot/uploads` altında şu klasörlerin olduğundan emin olun:
    `exhibitions`, `testimonials`, `projects`, `references`, `services`, `settings`.

---

##  Sorun Giderme (Özellikle Fotoğraf Yükleme Sorunu İçin)
* **Form Yapısı:** Formlarınızda `enctype="multipart/form-data"` olduğundan emin olun.
* **Dosya İzinleri:** Sunucuda veya yerel bilgisayarda `wwwroot/uploads` klasörüne yazma izni verin.
* **Uzantı Kontrolü:** Sadece `.jpg, .png, .webp, .svg` gibi izinli formatları kullandığınızdan emin olun.
* **Boyut Sınırı:** Büyük dosyalar için `Program.cs` içinden `MultipartBodyLengthLimit` değerini yükseltin.

---
