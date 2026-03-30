//# Генератор PDF для наклеек на Knight 80 (C#)

//using static System.Net.Mime.MediaTypeNames;

//Ниже пример полноценной программы на C#, которая создаёт PDF формата A4 с двумя наклейками для твоего Knight 80.

//Я не могу точно определить размеры наклеек только по фото, поэтому в начале кода есть блок с размерами в миллиметрах. Тебе нужно просто измерить реальные наклейки линейкой или штангенциркулем и вписать значения.

//Например:

//*stickerWidthMm — ширина одной наклейки
//* stickerHeightMm — высота
//* distanceBetweenMm — расстояние между ними

//```csharp
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        // === НАСТРОЙКИ РАЗМЕРОВ ===
        // Измени эти значения под свои реальные размеры
        double stickerWidthMm = 21.0;
        double stickerHeightMm = 40.0;
        double distanceBetweenMm = 4.0;

        // Поля листа
        double marginMm = 10.0;

        // Путь к изображениям наклеек
        string topStickerPath = "top.png";
        string bottomStickerPath = "bottom.png";

        // Имя PDF
        string outputFile = "Knight80_Stickers.pdf";

        PdfDocument document = new PdfDocument();
        document.Info.Title = "Knight 80 Stickers";

        PdfPage page = document.AddPage();

        // A4
        page.Width = XUnit.FromMillimeter(210);
        page.Height = XUnit.FromMillimeter(297);

        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Конвертация мм -> points
        double stickerWidth = XUnit.FromMillimeter(stickerWidthMm).Point;
        double stickerHeight = XUnit.FromMillimeter(stickerHeightMm).Point;
        double distanceBetween = XUnit.FromMillimeter(distanceBetweenMm).Point;
        double margin = XUnit.FromMillimeter(marginMm).Point;

        // Размеры страницы
        double pageWidth = page.Width.Point;
        double pageHeight = page.Height.Point;

        // Сколько наборов наклеек помещается на лист
        double blockWidth = stickerWidth;
        double blockHeight = stickerHeight * 2 + distanceBetween;

        int columns = (int)((pageWidth - margin * 2) / (blockWidth + margin));
        int rows = (int)((pageHeight - margin * 2) / (blockHeight + margin));

        if (!File.Exists(topStickerPath) || !File.Exists(bottomStickerPath))
        {
            Console.WriteLine("Not found: top.png or bottom.png");
            return;
        }

        XImage topImage = XImage.FromFile(topStickerPath);
        XImage bottomImage = XImage.FromFile(bottomStickerPath);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                double x = margin + col * (blockWidth + margin);
                double y = margin + row * (blockHeight + margin);

                // Верхняя наклейка
                gfx.DrawImage(topImage, x, y, stickerWidth, stickerHeight);

                // Нижняя наклейка
                gfx.DrawImage(
                    bottomImage,
                    x,
                    y + stickerHeight + distanceBetween,
                    stickerWidth,
                    stickerHeight);

                // Тонкая рамка для вырезания
                gfx.DrawRectangle(XPens.LightGray, x, y, stickerWidth, stickerHeight);
                gfx.DrawRectangle(
                    XPens.LightGray,
                    x,
                    y + stickerHeight + distanceBetween,
                    stickerWidth,
                    stickerHeight);
            }
        }

        document.Save(outputFile);

        Console.WriteLine($"PDF Create: {outputFile}");
    }
}

//## Как установить библиотеку

//Выполни в терминале проекта:

//```bash
//dotnet add package PdfSharp
//```

//## Какие файлы положить рядом с программой

//```text
//top.png
//bottom.png
//```

//* `top.png` — верхняя часть наклейки
//* `bottom.png` — нижняя часть

//## Как измерить правильно

//Для Knight 80 лучше измерить:

//1.ширину каждой панели
//2.высоту каждой панели
//3. расстояние между верхней и нижней частью

//Запиши размеры в миллиметрах и подставь в:

//```csharp
//stickerWidthMm
//stickerHeightMm
//distanceBetweenMm
//```

//Если хочешь, я могу ещё помочь сделать:

//*скруглённые углы как у оригинальной наклейки
//* автоматическую обрезку по форме
//* вставку сразу твоего изображения в нужные области
//* готовый шаблон именно по фото Knight 80
