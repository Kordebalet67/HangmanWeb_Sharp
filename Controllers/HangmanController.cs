using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HangmanWeb.Models;

namespace HangmanWeb.Controllers
{
    public class HangmanController : Controller
    {
        private readonly string[] _words = { 
            "ПРОГРАММА", "КОМПЬЮТЕР", "РАЗРАБОТКА", "АЛГОРИТМ", 
            "КЛАСС", "МЕТОД", "БИБЛИОТЕКА", "ФРЕЙМВОРК", "КОНТРОЛЛЕР" 
        };

        public IActionResult Index()
        {
            var state = new HangmanState
            {
                Word = _words[new Random().Next(_words.Length)]
            };
            return View(state);
        }

        [HttpPost]
        public IActionResult Guess(string word, string guessed, string wrong, string? letter)
        {
            // Если слово или буква потерялись, начинаем заново
            if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(letter))
            {
                return RedirectToAction("Index");
            }

            char l = letter.ToUpper()[0];
            
            // Восстанавливаем списки из строк, разделенных запятыми
            var guessedList = string.IsNullOrEmpty(guessed) 
                ? new List<char>() 
                : guessed.Split(',').Select(c => c[0]).ToList();
                
            var wrongList = string.IsNullOrEmpty(wrong) 
                ? new List<char>() 
                : wrong.Split(',').Select(c => c[0]).ToList();

            // Если букву еще не называли
            if (!guessedList.Contains(l))
            {
                guessedList.Add(l);
                
                // Если буквы нет в слове, добавляем в ошибки
                if (!word.Contains(l))
                {
                    wrongList.Add(l);
                }
            }

            // Собираем состояние обратно для передачи в View
            var state = new HangmanState
            {
                Word = word,
                Guessed = guessedList,
                Wrong = wrongList
            };

            return View("Index", state);
        }
    }
}