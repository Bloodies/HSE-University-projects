import numpy
import requests
import telebot
import tensorflow
import time
from flask import Flask
from keras.models import load_model
from telebot import types

token = ''
bot = telebot.TeleBot(token)
app = Flask(__name__)
model = load_model('NeuroSet')

user_dict = {}
state, oscars_sum = 0, 0
genre, mpaa, franchise, season, holiday = 0, 0, 0, 0, 0
director_rating, dirawards, wrawards, stawards = 0, 0, 0, 0
budget, duration, oscars = 0, 0, 0

# Клавиатура меню
main_menu = types.ReplyKeyboardMarkup(resize_keyboard=True, one_time_keyboard=False)
btn_info = types.KeyboardButton('Информация')
btn_cur = types.KeyboardButton('Спрогнозировать')
main_menu.row(btn_info, btn_cur)

no = types.InlineKeyboardButton('Нет', callback_data='0')
yes = types.InlineKeyboardButton('Да', callback_data='1')

empty = telebot.types.ReplyKeyboardRemove()
msg = None

info_msg = '''
Бот разработан учащимся 4 курса Чепоковым Елизаром (ПИ-18-2)
В рамках выпускной квалификационной работы
Бот предназначен для прогнозирования кассовых сборов фильма
'''


@bot.message_handler(content_types=['text'])
def start(message):
    global state, msg, genre, mpaa, franchise, season, holiday, director_rating, dirawards, wrawards, stawards, budget, duration, oscars

    bot.send_chat_action(message.chat.id, 'typing')
    if message.text == '/start':
        bot.send_message(message.chat.id, 'Приветствую', reply_markup=main_menu)
    elif message.text == 'Информация':
        bot.send_message(message.chat.id, info_msg, parse_mode="markdown")
    elif message.text == 'Спрогнозировать':
        state, budget, duration, oscars, oscars_sum = 0, 0, 0, 0, 0
        bot.send_message(message.chat.id,
                         'Введите значение бюджета в диапазоне от 100000 до 100000000',
                         reply_markup=empty)
    else:
        if state == 0:
            budget = int(message.text) if str(message.text).isdigit() else -1
            if 100000 <= budget <= 100000000:
                state = 1
                bot.send_message(message.chat.id, 'Введите длительность фильма в диапазоне от 50 до 300 (фильмы '
                                                  'длительностью меньше 50 мин являются короткометражными)')
            else:
                bot.send_message(message.chat.id, 'Введите целое число от 100000 до 100000000')
        elif state == 1:
            duration = int(message.text) if str(message.text).isdigit() else -1
            if 50 <= duration <= 300:
                state = 2

                genre_markup = types.InlineKeyboardMarkup()
                action = types.InlineKeyboardButton('Боевик (Action)', callback_data='1')
                adventure = types.InlineKeyboardButton('Приключение (Adventure)', callback_data='2')
                drama = types.InlineKeyboardButton('Драма', callback_data='3')
                comedy = types.InlineKeyboardButton('Комедия', callback_data='4')
                criminal = types.InlineKeyboardButton('Криминальный фильм', callback_data='5')
                mystery = types.InlineKeyboardButton('Мистика', callback_data='6')
                horror = types.InlineKeyboardButton('Хоррор', callback_data='7')
                history = types.InlineKeyboardButton('Исторический', callback_data='8')
                documentary = types.InlineKeyboardButton('Документальный', callback_data='9')
                animation = types.InlineKeyboardButton('Анимация', callback_data='10')
                fantasy = types.InlineKeyboardButton('Фантастика', callback_data='11')
                thriller = types.InlineKeyboardButton('Триллер', callback_data='12')
                musical = types.InlineKeyboardButton('Мюзикл', callback_data='13')

                genre_markup.row(action, adventure)
                genre_markup.row(drama, comedy, criminal)
                genre_markup.row(mystery, horror, history)
                genre_markup.row(documentary, animation, fantasy)
                genre_markup.row(thriller, musical)

                msg = bot.send_message(message.chat.id, 'Выберите жанр:', reply_markup=genre_markup)
            else:
                bot.send_message(message.chat.id, 'Введите целое число от 50 до 300 (фильмы '
                                                  'длительностью меньше 50 мин являются короткометражными)')
        elif state == 11:
            oscars = int(message.text) if str(message.text).isdigit else -1
            if 0 <= oscars <= 30:
                print(mpaa, duration, season, holiday, dirawards, wrawards, stawards, oscars, genre, franchise, budget)
                output = numpy.array([[mpaa, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, budget],
                                      [mpaa, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, int(budget - ((budget / 100) * 5))],
                                      [mpaa, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, int(budget - ((budget / 100) * 10))],
                                      [mpaa, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, int(budget + ((budget / 100) * 5))],
                                      [mpaa, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, int(budget + ((budget / 100) * 10))],
                                      [mpaa, duration, season, 0 if holiday == 1 else 1, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, budget],
                                      [mpaa, duration, season, holiday, 0 if dirawards == 1 else 1, wrawards,
                                       stawards, oscars, genre, franchise, budget],
                                      [mpaa, duration, season, holiday, dirawards, 0 if wrawards == 1 else 1,
                                       stawards, oscars, genre, franchise, budget],
                                      [mpaa, duration, season, holiday, dirawards, wrawards,
                                       0 if stawards == 1 else 1, oscars, genre, franchise, budget],
                                      [1 if mpaa != 1 else 2, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, budget],
                                      [3 if mpaa != 3 else 4, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, budget],
                                      [4 if mpaa != 4 else 5, duration, season, holiday, dirawards, wrawards,
                                       stawards, oscars, genre, franchise, budget]])

                predictions = model.predict(output)

                result = 0
                pred = round((predictions[0, 0]) / 10) * 10
                if pred <= 10:
                    result = '500.000'
                elif 11 < pred <= 20:
                    result = '1.000.000'
                elif 21 < pred <= 30:
                    result = '2.500.000'
                elif 31 < pred <= 40:
                    result = '5.000.000'
                elif 41 < pred <= 50:
                    result = '7.500.000'
                elif 51 < pred <= 60:
                    result = '10.000.000'
                elif 61 < pred <= 70:
                    result = '15.000.000'
                elif 71 < pred <= 80:
                    result = '20.000.000'
                elif 81 < pred <= 90:
                    result = '30.000.000'
                elif 91 <= pred:
                    result = '40.000.000'

                bot.send_message(message.chat.id, f'Прогнозируемые кассовые сборы: ~${result}', reply_markup=main_menu)
                state = 0
            else:
                bot.send_message(message.chat.id, 'Введите целое число от 0 до 30')
        else:
            bot.send_message(message.chat.id, 'Команда не найдена', reply_markup=main_menu)
            state = 0
            pass


@bot.callback_query_handler(lambda call: True)
def handle(call):
    global state, msg, genre, mpaa, franchise, season, holiday, director_rating, dirawards, wrawards, stawards, oscars_sum

    if state == 2:
        genre = int(call.data) if str(call.data).isdigit() else 1
        state = 3

        bot.delete_message(call.message.chat.id, msg.message_id)
        mpaa_markup = types.InlineKeyboardMarkup()
        g = types.InlineKeyboardButton('0+', callback_data='1')
        pg = types.InlineKeyboardButton('6+', callback_data='2')
        pg13 = types.InlineKeyboardButton('12+', callback_data='3')
        nc17 = types.InlineKeyboardButton('16+', callback_data='4')
        r = types.InlineKeyboardButton('18+', callback_data='5')

        mpaa_markup.row(g, pg, pg13, nc17, r)
        msg = bot.send_message(call.message.chat.id, 'Выберите возрастной рейтинг:', reply_markup=mpaa_markup)

        bot.answer_callback_query(call.id)
    elif state == 3:
        mpaa = int(call.data) if str(call.data).isdigit() else 1
        state = 4

        bot.delete_message(call.message.chat.id, msg.message_id)
        franchise_markup = types.InlineKeyboardMarkup()
        franchise_markup.row(no, yes)
        msg = bot.send_message(call.message.chat.id, 'Является ли фильм продолжением кинофраншизы?',
                               reply_markup=franchise_markup)

        bot.answer_callback_query(call.id)
    elif state == 4:
        franchise = int(call.data) if str(call.data).isdigit() else 1
        state = 5

        bot.delete_message(call.message.chat.id, msg.message_id)
        season_markup = types.InlineKeyboardMarkup()
        winter = types.InlineKeyboardButton('Зима', callback_data='4')
        autumn = types.InlineKeyboardButton('Весна', callback_data='1')
        summer = types.InlineKeyboardButton('Лето', callback_data='2')
        spring = types.InlineKeyboardButton('Осень', callback_data='3')

        season_markup.row(winter, autumn)
        season_markup.row(summer, spring)
        msg = bot.send_message(call.message.chat.id, 'Выберите сезон выхода:', reply_markup=season_markup)

        bot.answer_callback_query(call.id)
    elif state == 5:
        season = int(call.data) if str(call.data).isdigit() else 1
        state = 6

        bot.delete_message(call.message.chat.id, msg.message_id)
        holiday_markup = types.InlineKeyboardMarkup()
        holiday_markup.row(no, yes)
        msg = bot.send_message(call.message.chat.id, 'Выход запланирован в праздники?', reply_markup=holiday_markup)

        bot.answer_callback_query(call.id)
    elif state == 6:
        holiday = int(call.data) if str(call.data).isdigit() else 1
        state = 7

        bot.delete_message(call.message.chat.id, msg.message_id)
        rating_markup = types.InlineKeyboardMarkup()
        num1 = types.InlineKeyboardButton('1', callback_data='1')
        num2 = types.InlineKeyboardButton('2', callback_data='2')
        num3 = types.InlineKeyboardButton('3', callback_data='3')
        num4 = types.InlineKeyboardButton('4', callback_data='4')
        num5 = types.InlineKeyboardButton('5', callback_data='5')
        num6 = types.InlineKeyboardButton('6', callback_data='6')
        num7 = types.InlineKeyboardButton('7', callback_data='7')
        num8 = types.InlineKeyboardButton('8', callback_data='8')
        num9 = types.InlineKeyboardButton('9', callback_data='9')
        num10 = types.InlineKeyboardButton('10', callback_data='10')

        rating_markup.row(num1, num2, num3)
        rating_markup.row(num4, num5, num6)
        rating_markup.row(num7, num8, num9)
        rating_markup.row(num10)
        msg = bot.send_message(call.message.chat.id, 'Введите рейтинг предыдущих работ режиссера:',
                               reply_markup=rating_markup)

        bot.answer_callback_query(call.id)
    elif state == 7:
        director_rating = int(call.data) if str(call.data).isdigit() else 5
        state = 8

        bot.delete_message(call.message.chat.id, msg.message_id)
        dirawards_markup = types.InlineKeyboardMarkup()
        dirawards_markup.row(no, yes)
        msg = bot.send_message(call.message.chat.id,
                               'Имеются ли у режиссера престижные награды («Оскар», «Золотой глобус», SAAG, Critics’ Choice Awards)?',
                               reply_markup=dirawards_markup)

        bot.answer_callback_query(call.id)
    elif state == 8:
        dirawards = int(call.data) if str(call.data).isdigit() else 1
        if dirawards > 0:
            oscars_sum += 1

        state = 9

        bot.delete_message(call.message.chat.id, msg.message_id)
        wrawards_markup = types.InlineKeyboardMarkup()
        wrawards_markup.row(no, yes)
        msg = bot.send_message(call.message.chat.id,
                               'Имеются ли у сценариста престижные награды («Оскар», «Золотой глобус», SAAG, Critics’ Choice Awards)?',
                               reply_markup=wrawards_markup)

        bot.answer_callback_query(call.id)
    elif state == 9:
        wrawards = int(call.data) if str(call.data).isdigit() else 1
        if wrawards > 0:
            oscars_sum += 1

        state = 10

        bot.delete_message(call.message.chat.id, msg.message_id)
        stawards_markup = types.InlineKeyboardMarkup()
        stawards_markup.row(no, yes)
        msg = bot.send_message(call.message.chat.id,
                               'Имеются ли у актеров престижные награды («Оскар», «Золотой глобус», SAAG, Critics’ Choice Awards)?',
                               reply_markup=stawards_markup)

        bot.answer_callback_query(call.id)
    elif state == 10:
        stawards = int(call.data) if str(call.data).isdigit() else 1
        if stawards > 0:
            oscars_sum += 1

        state = 11

        bot.delete_message(call.message.chat.id, msg.message_id)
        if oscars_sum > 0:
            msg = bot.send_message(call.message.chat.id, 'Введите количество оскаров в диапазоне от 0 до 30')
        else:
            msg = bot.send_message(call.message.chat.id, 'Введите 0 (вы указали что престижных наград нет)')
        bot.answer_callback_query(call.id)
    else:
        state = 0
        bot.send_message(call.message.chat.id, 'Бот дал сбой (начните заново)', reply_markup=main_menu)
        bot.answer_callback_query(call.id)
        pass


bot.remove_webhook()
# bot.set_webhook('https://test.com/' + token)
# app.run()

while True:
    try:
        bot.polling(none_stop=True, interval=0, timeout=20)
    except Exception as E:
        print(E.args)
        time.sleep(2)
