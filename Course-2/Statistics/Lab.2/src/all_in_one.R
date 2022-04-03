task_1 <- function()
{
  income <-  as.numeric(readline("Доход этого месяца: "))
  #log_income
  income_pre <- 500000
  if(income_pre>income)
  {
    cat("В предыдущем месяце доход был больше на ", (income_pre-income))
  }else if (income_pre<income)
  {
    cat("В этом месяце доход был больше на ", (income-income_pre))
  }else
  {
    print("Доход одинаков")
  }
}

task_2 <- function()
{
  x <- as.numeric(readline("x: "))
  y <- as.numeric(readline("y: "))
  x_exch <- y
  y_exch <- x
  y <- y_exch
  x <- x_exch
  print(x)
  print(y)
}

task_3 <- function()
{
  x <- 3.5    # numeric
  y <- "2,6"  # character
  z <- 1.78   # numeric
  h <- TRUE   # logical
  class(x);
  class(y);
  class(z);
  class(h);
}

task_4 <- function()
{
  q <- c(4, 7, -1, 21, 2, 0, 14)
  q_sq <- q^2
  print(q_sq)
  q_log <- log(q,exp(1))
  print(q_log)
  print(q[q >= 0])
  which(q%%7 == 0)
  print(q_log[q_log %%2 == 0] & q_log[q_log > 5])
}

task_5 <- function()
{
  turnout <- c(100, 124, 121, 130, 150, 155, 144, 132, 189, 145, 125, 110, 118, 129, 127)
  which((turnout %%5 == 0) | (turnout %%10 == 0))
  index <- length(turnout[turnout %%5 == 0])
  print(index)
  dole <- length(turnout)
  print(dole)
  part <- round((index/dole)*100, 2)
}

task_6 <- function()
{
  z <- c(8, NA, 7, 10, NA, 15, NA, 0, NA, NA, 87)
  which(is.na(z))
}

task_7 <- function()
{
  s <- c("4,5", "6,8", "9,2", "1,75")
  n <- as.numeric(gsub(",", ".", s))
  cat(n)
}

task_8 <- function()
{
  Y <- c(1, 50, 1, 75);
  dim(Y) <- c(2, 2);
  Z <- c(100, 6625);
  dim(Z) <- c(2, 1);
  cat("Решение системы уравнений\n")
  print(solve(Y, Z))
  A <- c(1, 2, 3, 4, 2, 7, 6, 9, 3, 6, 3, 8, 4, 9, 8, 2); 
  dim(A) <- c(4, 4); 
  cat("Обратная матрица\n")
  print(solve(A))
  cat("Транспонированная матрица\n")
  print(t(A))
  cat("Сумма диагональных элементов\n")
  print(sum(diag(A)))
  cat("Определитель матрицы\n")
  print(det(A))
  Ax <- c(1, 3, 4, 2, 6, 9, 4, 8, 2); 
  dim(Ax) <- c(3, 3);
  cat("Алгебраическое дополнение к элементу A[2,3]\n")
  print(det(-Ax))
}