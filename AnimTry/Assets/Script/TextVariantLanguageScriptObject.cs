using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;

public class TextVariantLanguageScriptObject 
{
    public string ItemNameLocalization(Item item)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = item.itemName_ENG;
        else
            phrace = item.itemName_RU;

        return phrace;
    }

    public string ItemDescriptionLocalization(Item item)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = item.description_ENG;
        else
            phrace = item.description_RU;

        return phrace;
    }

    public string ItemTypeLocalization(Item item)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = item.type.ToString();
        else
        {
            if (item.type==Item.Type.Pressure)
                phrace = "Давление";
            else
                phrace = "Выносливость";
        }
        return phrace;
    }

    public string IngridientTitleLocalization(Ingridient ingridient)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = ingridient.TitleEng;
        else
            phrace = ingridient.TitleRus;

        return phrace;
    }

    public string HeroNameLocalization(BaseHero baseHero)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = baseHero.heroNameEng;
        else
            phrace = baseHero.heroNameRus;

        return phrace;
    }

    public string[] HeroStatLocalization()
    {
        string[] phrace;

        if (LocalizationManager.Language.Equals("English"))
            phrace = new string[6]{ "HotShop: ", "ColdShop: ", "Confectioner: ", "Pressure: ", "Stamina: ", "Return: " };
        else
            phrace = new string[6] { "Горячий цех: ", "Холодный цех: ", "Кондитер: ", "Давление: ", "Выносливость: ", "Возвраты: " };

        return phrace;
    }

    public string FoodNameLocalization(Food food)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = food.foodName_ENG;
        else
            phrace = food.foodName_RUS;

        return phrace;
    }

    public string FoodDescriptionLocalization(Food food)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = food.description_ENG;
        else
            phrace = food.description_RUS;

        return phrace;
    }

    public string FoodTypeLocalization(Food food)
    {
        string phrace = "";

            switch (food.typeOfFood)
            {
                case Food.Type.ColdShop:
                    {
                        phrace = HeroStatLocalization()[1];
                    }
                    break;
                case Food.Type.HotShop:
                    {
                        phrace = HeroStatLocalization()[0];
                    }
                    break;
                case Food.Type.Confectioner:
                    {
                        phrace = HeroStatLocalization()[2];
                    }
                    break;
            }

        return phrace;
    }

    public string BookTitleLocalization(BooksWithStats book)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = book.BookTitle_ENG;
        else
            phrace = book.BookTitle_RUS;

        return phrace;
    }

    public string BookTypeLocalization(BooksWithStats book)
    {
        string phrace = "";

        switch (book.type)
        {
            case BooksWithStats.TypeOfStat.ColdShop:
                {
                    phrace = HeroStatLocalization()[1];
                }
                break;
            case BooksWithStats.TypeOfStat.HotShop:
                {
                    phrace = HeroStatLocalization()[0];
                }
                break;
            case BooksWithStats.TypeOfStat.Confectioner:
                {
                    phrace = HeroStatLocalization()[2];
                }
                break;
        }

        return phrace;
    }

    public string BookLootLocalization(BooksWithStats book)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = "You picked up a book: " + book.BookTitle_ENG + "\n\n" + "Skill: " + book.type + " your team has grown by " + book.count + ".";
        else
            phrace = "Ты подобрал книгу: " + book.BookTitle_RUS + "\n\n" + "Навык: " + BookTypeLocalization(book) + " твоей команды вырос на " + book.count + "."; 

        return phrace;
    }

    public string QuestTitleLocalization(Quest quest)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = quest.QuestTitle_ENG;
        else
            phrace = quest.QuestTitle_RUS;

        return phrace;
    }

    public string QuestDescriptionLocalization(Quest quest)
    {
        string phrace = "";

        if (LocalizationManager.Language.Equals("English"))
            phrace = quest.QuestDescription_ENG;
        else
            phrace = quest.QuestDescription_RUS;

        return phrace;
    }
}
