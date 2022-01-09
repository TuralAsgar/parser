# Guide

<details>
  <summary>Technical requirements</summary>

Texniki tapşırıq aşağıda diqqətinizə çatdırılır:
Texniki tapşırıq olaraq “File To DB parser” yazmağınız gərəklidir.

Bunun üçün sizə bir “transactions.txt” adında bir fayl təqdim edirik.

Bu fayl içərisi xüsusi strukturda yazılmış əməliyyatların siyahısı qeyd olunmuşdur.

Sizdən gözləntimiz bu məlumatları eyni strukturla MSSql database-nə yazmağınızdır.

- Proqram WEB application olmalıdır
- İstifadəçi ekrandakı fayl seçimi yerinə tıklayıb faylı proqrama yükləməlidir.
- Yüklədikdən sonra məlumatları ekranda göstər düyməsinə basdıqda proqram fayldakı məlumatları bazadakı cədvələ yazmalıdır.
- Sonra sql sorğusu vasitəsilə cədvəldəki məlumatları grid şəklində ekranda göstərməlidir.

Tapşırığı həll edib, 10.01.2022-ci il tarixində geri göndərməyiniz Sizdən xahiş olunur.

</details>

## Usage

1. Clone repo `https://github.com/tural-esger/parser.git`
2. Create a database called `parser` in SQL server
3. Import `db.sql` into `parser` database
4. Check connection string in `appsettings.json`
5. Go to project directory (`cd Parser`)
6. Restore dependencies `dotnet restore`
7. Run application `dotnet run`
8. Open localhost link in the browser

### Optional
If you don't want to install SQL server and import tables (step 2 and 3), you can use Docker

1. Go to Docker/sql-server directory `cd ./Docker/sql-server`
2. Run `docker compose up`