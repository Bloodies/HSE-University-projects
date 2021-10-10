inputNumber <- function(promt)
{
  while (TRUE)
  {
    num = suppressWarnings(as.numeric(readline(promt)))
    if (!is.na(num))
    {
      break
    }
  }
}

displayMenu <- function(options)
{
  write("---------------------------")
  for (i in 1:length(options))
  {
    cat(sprintf("%d. %s\n", i, options[i]))
  }
  write("---------------------------")
  choice <- 0
  while (!any(choice == 1:length(options)))
  {
    choice = inputNumber("Выберите часть задания: ")
  }
  return(choice)
}

program <- function() 
{
  menu_1 <- c("Part 1", "Part 2", "Quit")
  menu_1_1 <- c("Task 1", "Task 2", "Task 3", "Main menu", "Quit")
  menu_1_2 <- c("Task 1", "Task 2", "Task 3", "Task 4", "Task 5", "Task 6", "Task 7", "Main menu", "Quit")
  
  name <- ""
  while (TRUE)
  {
    choice_1 <- displayMenu(menu_1)
    if (choice_1 == 1)
    {
      while (TRUE)
      {
        choise_1_1 <- displayMenu(menu_1_1)
        if (choice_1 == 1)
        {
          
        }else if (choise_1_1 == 2)
        {
          
        }else if (choise_1_1 == 3)
        {
          
        }else if (choise_1_1 == 4)
        {
          program()
        }else if (choise_1_1 == 5)
        {
          break
        }
      }
    }else if (choice_1 == 2)
    {
      while (TRUE)
      {
        choise_1_2 <- displayMenu(menu_1_2)
        if (choise_1_2 == 1)
        {
          
        }else if (choise_1_2 == 2)
        {
          
        }else if (choise_1_2 == 3)
        {
          
        }else if (choise_1_2 == 4)
        {
          
        }else if (choise_1_2 == 5)
        {
          
        }else if (choise_1_2 == 6)
        {
          
        }else if (choise_1_2 == 7)
        {
          
        }else if (choise_1_2 == 8)
        {
          program()
        }else if (choice_1 == 9)
        {
          break
        }
      }
    }else if (choice_1 == 3)
    {
      break
    }
  }
}
