#install.packages(c("xlsx", "dplyr", "moments", "psych", "ggplot2", "DescTools"))
library(ggplot2)
library(DescTools)
library(xlsx)
library(dplyr)
library(moments)
library(psych)
Task <- function()
{
cat("\n- 1 -------------------------")
df <- read.csv("https://raw.githubusercontent.com/allatambov/R-programming-3/master/seminars/sem8-09-02/demography.csv", encoding = "UTF-8")
cat("\n-----------------------------")

cat("\n- 2 -------------------------")
df$young_share <- as.double(round(df$young_total/df$popul_total*100, 2))
df$trud_share <- as.double(round(df$wa_total/df$popul_total*100, 2))
df$old_share <- as.double(round(df$ret_total/df$popul_total*100, 2))
cat("\n-----------------------------")

cat("\n- 3 -------------------------")
ggplot(data = df, aes(x = trud_share)) +
  geom_histogram(binwidth = 0.3, fill = "green", color = "black") +
  labs(x = "Процент", y = "Частота", title = "Трудоспособное население") +
  geom_vline(xintercept = median(df$trud_share),color = "red") +
  geom_rug()
cat("\n-----------------------------")

cat("\n- 4 -------------------------")
print(ggplot(data = df, aes(x = trud_share, group = region, fill = region)) +
        geom_density(alpha = 0.5) + 
        geom_rug() + 
        labs(x = "Процент", title = "Плотность распределения") + 
        scale_fill_manual(values = c("black","blue")) + 
        scale_fill_discrete(name = "Регион"))

print(ggplot(data = df, aes(x = "", y = trud_share, group = region, fill = region)) +
        geom_violin() + 
        geom_rug() + 
        labs(x = "Плотность", y = "Процент") + 
        scale_fill_manual(values = c("red","green")) + 
        scale_fill_discrete(name = "Регион"))
cat("\n-----------------------------")

cat("\n- 5 -------------------------")
print(ggplot(data = df, aes(x = young_share, y = old_share)) +
  geom_point(color = "blue",pch = 11) + 
  labs(title = "Диаграмма рассеивания", x = "Процент молодого начеления", y = "Процент пожилого населения"))

cat("\n-----------------------------")


cat("\n- 6 -------------------------")
df$male_share <- as.double(round((df$wa_male + df$ret_male + df$young_male)/df$popul_total * 100, 2))
df$male <- as.integer(df$male_share > 50)
cat("\n-----------------------------")

cat("\n- 7 -------------------------")
print(ggplot(data = df, aes(x = young_share, y = old_share)) +
  geom_point(aes(size = male_share, color = male)) + 
  labs(title = "Диаграмма рассеивания", x = "Процент молодого населения", y = "Процент пожилого населения"))
cat("\n-----------------------------")

cat("\n- 8 -------------------------")
ggplot(df, aes(x = factor(region))) +
  geom_bar(stat = "count", width = 0.5, fill = "red")
cat("\n-----------------------------")
}