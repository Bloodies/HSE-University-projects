# nltk.download()
import nltk
import pymorphy2
import string
from nltk.corpus import stopwords

open('output.txt', 'w').close()


def Tokens(text):
    tokens = nltk.word_tokenize(text)
    tokens = [i for i in tokens if (i not in string.punctuation)]

    stop_words = stopwords.words('russian')
    stop_words.extend(['что', 'это', 'так', 'вот', 'быть', 'как', 'в', '-', 'к', 'на', '...'])
    tokens = [i for i in tokens if (i not in stop_words)]
    print(tokens)
    return tokens


with open("input.txt", "r", encoding='utf-8') as file:
    for line in file:
        line = Tokens(line)
        with open("output.txt", "a", encoding='utf-8') as output:
            for word in line:
                output.write(word + '\n')
