﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.DBFunction;

namespace WebApp.Pages.Diaries
{
    public class CreateModel : PageModel
    {
        private readonly WebAppDiaryContext _context;
        public SelectList _DiaryType;

        public CreateModel(WebAppDiaryContext context)
        {
            _context = context;
            _DiaryType = new SelectList(_context.DiariesTypes.ToList(), "ID", "Type");
        }

        [BindProperty]
        public Diary Diary { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Diary.DiaryType = Gets.GetDiaryType(Diary.DiaryType.ID, _context);

            _context.Diaries.Add(Diary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
