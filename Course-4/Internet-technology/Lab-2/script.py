# nltk.download()
# nltk.download('punkt')
# nltk.download('stopwords')
import nltk
import pymorphy2
import string
from nltk.probability import FreqDist
from nltk.corpus import stopwords
from nltk.stem.snowball import SnowballStemmer
from nltk import tokenize
from collections import OrderedDict

file = open('input.txt', "r", encoding="utf-8")
input_text = file.read()
file.close()

stop_words = stopwords.words('russian')
spec_chars = string.punctuation + '\n\xa0«»\t—…-...'  # удаляем специальные символы
stemmer = SnowballStemmer("russian")
temp_tf_idf = FreqDist


def remove_chars_from_text(word, chars):
    return "".join([ch for ch in word if ch not in chars])


text = input_text.lower()

text = remove_chars_from_text(text, spec_chars)
text = remove_chars_from_text(text, string.digits)
text_tokens = nltk.word_tokenize(text)

text_tokens = [token.strip() for token in text_tokens if token not in stop_words]


def Task_1():
    morph = pymorphy2.MorphAnalyzer()  # нормализация
    for index, t in enumerate(text_tokens):
        text_tokens[index] = morph.parse(t)[0].normal_form

    new_text = nltk.Text(text_tokens)  # преобразуем в текст

    fdist = FreqDist(new_text)  # получаем частотное распределение

    output = open('output_1.txt', "w", encoding="utf-8")
    for f in fdist:
        output.write(f + " " + fdist[f].__str__() + '\n')


def Task_2():  # нормализация
    global temp_tf_idf
    for index, t in enumerate(text_tokens):
        text_tokens[index] = stemmer.stem(t)

    new_text = nltk.Text(text_tokens)  # преобразуем в текст

    fdist = FreqDist(new_text)  # получаем частотное распределение
    temp_tf_idf = FreqDist(new_text)

    output = open('output_2.txt', "w", encoding="utf-8")
    for f in fdist:
        output.write(f + " " + fdist[f].__str__() + '\n')


def Task_3():  # используем нормализацию второго типа
    global temp_tf_idf
    tf = temp_tf_idf.copy()

    # считаем tf по формуле
    for i in tf:
        tf[i] = tf[i] / float(len(text_tokens))

    idf = temp_tf_idf.copy()
    for i in idf:
        idf[i] = 1

    tf_idf = temp_tf_idf.copy()
    for i in tf_idf:
        tf_idf[i] = tf[i] * idf[i]

    tf_idf = OrderedDict(sorted(tf_idf.items(), key=lambda kv: kv[1], reverse=True))
    temp_tf_idf = tf_idf

    output = open('output_3.txt', "w", encoding="utf-8")
    for f in tf_idf:
        output.write(f + " " + tf_idf[f].__str__() + '\n')


def Task_4():
    new_text = remove_chars_from_text(input_text, string.digits)

    sentences = tokenize.sent_tokenize(new_text)  # делим на предложения

    # функция получения ключа по значению
    def get_key(d, value):
        for v, k in d.items():
            if v == value:
                return k
        return 0

    sdict = dict()

    # алгоритм
    for i, s in enumerate(sentences):
        sentence = sentences[i]  # получаем текущее предложение
        sentence.lower()  # убираем регистр
        sentence = remove_chars_from_text(sentence, spec_chars)  # удаляем числа и символы
        sentence = remove_chars_from_text(sentence, string.digits)
        sentence_tokens = nltk.word_tokenize(sentence)  # токенизируем предложения
        sentence_tokens = [token.strip() for token in sentence_tokens if token not in stop_words]
        for index, t in enumerate(sentence_tokens):  # нормализация с помощью стиммера
            sentence_tokens[index] = stemmer.stem(t)

        weight = 0.0  # рассчитываем вес предложения

        for j, t in enumerate(sentence_tokens):
            weight = weight + get_key(temp_tf_idf, t)
        sdict[weight] = sentences[i]

    # сортировка по значению
    sdict_sort = OrderedDict(sorted(sdict.items(), key=lambda kv: kv[0], reverse=True))

    # ищем порог веса, который отвечает за то, включаем мы предложение или нет
    limit = list(sdict_sort.keys())[round((len(sdict_sort)) * 0.2)]

    output = open('output_4.txt', "w", encoding="utf-8")
    for f in sdict_sort:
        output.write(f.__str__() + " " + sdict_sort[f] + " " + '\n')

    for i, s in enumerate(sdict):
        if s >= limit:
            output.write(sdict[s] + '\n')


if __name__ == '__main__':
    Task_1()
    Task_2()
    Task_3()
    Task_4()
