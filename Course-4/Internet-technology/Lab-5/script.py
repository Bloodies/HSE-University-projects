import telebot
import os
import time
import requests
import platform
import ctypes
import mouse
import PIL.ImageGrab
from PIL import Image, ImageGrab, ImageDraw
from telebot import types

bot_token = ''
my_id =

bot = telebot.TeleBot(bot_token)

user_dict = {}


class User:
    def __init__(self):
        keys = ['urldown', 'fin', 'curs']

        for key in keys:
            self.key = None


User.curs = 50

menu_keyboard = types.ReplyKeyboardMarkup(resize_keyboard=True, one_time_keyboard=False)
btnstart = types.KeyboardButton('Запустить процесс')
btnkill = types.KeyboardButton('Завершить процесс')
btndown = types.KeyboardButton('Скачать файл с ПК')
btnscreen = types.KeyboardButton('Сделать скриншот')
btninfo = types.KeyboardButton('О компьютере')
btnoff = types.KeyboardButton('Выключить компьютер')
btnreb = types.KeyboardButton('Перезагрузить компьютер')
btnslp = types.KeyboardButton('Перевести в режим сна')
menu_keyboard.row(btnstart, btnkill)
menu_keyboard.row(btndown, btnscreen, btninfo)
menu_keyboard.row(btnoff, btnreb, btnslp)

MessageBox = ctypes.windll.user32.MessageBoxW
if os.path.exists("msg.pt"):
    pass
else:
    bot.send_message(my_id,
                     "Сообщение?",
                     parse_mode="markdown")
    MessageBox(None,
               f'На компьютере запущена программа управления через Telegram',
               '!ВНИМАНИЕ!', 0)
    f = open('msg.pt', 'tw', encoding='utf-8')
    f.close()
bot.send_message(my_id, "ПК запущен", reply_markup=menu_keyboard)


@bot.message_handler(content_types=["text"])
def get_text_messages(message):
    if message.from_user.id == my_id:
        bot.send_chat_action(my_id, 'typing')

        if message.text == "Запустить процесс":
            bot.send_message(my_id, "Укажите путь до файла: ")
            bot.register_next_step_handler(message, start_process)

        elif message.text == "Завершить процесс":
            bot.send_message(my_id, "Укажите название процесса: ")
            bot.register_next_step_handler(message, kill_process)

        elif message.text == "Скачать файл с ПК":
            bot.send_message(my_id, "Укажите путь до файла: ")
            bot.register_next_step_handler(message, downloadfile_process)

        elif message.text == "Сделать скриншот":
            bot.send_chat_action(my_id, 'upload_photo')
            try:
                currentMouseX, currentMouseY = mouse.get_position()
                img = PIL.ImageGrab.grab()
                img.save("screen.png", "png")
                img = Image.open("screen.png")
                draw = ImageDraw.Draw(img)
                draw.polygon((currentMouseX, currentMouseY, currentMouseX, currentMouseY + 15, currentMouseX + 10,
                              currentMouseY + 10), fill="white", outline="black")
                img.save("screen_with_mouse.png", "PNG")
                bot.send_photo(my_id, open("screen_with_mouse.png", "rb"))
                os.remove("screen.png")
                os.remove("screen_with_mouse.png")
            except:
                bot.send_message(my_id, "Компьютер заблокирован")

        elif message.text == "О компьютере":
            req = requests.get('http://ip.42.pl/raw')
            ip = req.text
            uname = os.getlogin()
            windows = platform.platform()
            processor = platform.processor()
            # print(*[line.decode('cp866', 'ignore') for line in Popen('tasklist', stdout=PIPE).stdout.readlines()])
            bot.send_message(my_id, f"*Пользователь:* {uname}\n*IP:* {ip}\n*ОС:* {windows}\n*Процессор:* {processor}",
                             parse_mode="markdown")

            bot.register_next_step_handler(message, get_text_messages)

        elif message.text == "Выключить компьютер":
            bot.send_message(my_id, "Выключение компьютера...")
            os.system('shutdown -s /t 0 /f')

            bot.register_next_step_handler(message, get_text_messages)

        elif message.text == "Перезагрузить компьютер":
            bot.send_message(my_id, "Перезагрузка компьютера...")
            os.system('shutdown -r /t 0 /f')

            bot.register_next_step_handler(message, get_text_messages)

        elif message.text == "Перевести в режим сна":
            bot.send_message(my_id, "Перезагрузка компьютера...")
            os.system('shutdown -r /t 0 /f')

            bot.register_next_step_handler(message, get_text_messages)

        else:
            pass

    else:
        info_user(message)


def info_user(message):
    bot.send_chat_action(my_id, 'typing')
    alert = f"Кто-то пытался задать команду: \"{message.text}\"\n\n"
    alert += f"user id: {str(message.from_user.id)}\n"
    alert += f"first name: {str(message.from_user.first_name)}\n"
    alert += f"last name: {str(message.from_user.last_name)}\n"
    alert += f"username: @{str(message.from_user.username)}"
    bot.send_message(my_id, alert, reply_markup=menu_keyboard)


def kill_process(message):
    bot.send_chat_action(my_id, 'typing')
    try:
        os.system("taskkill /IM " + message.text + " -F")
        bot.send_message(my_id, f"Процесс \"{message.text}\" убит", reply_markup=menu_keyboard)
        bot.register_next_step_handler(message, get_text_messages)
    except:
        bot.send_message(my_id, "Ошибка! Процесс не найден", reply_markup=menu_keyboard)
        bot.register_next_step_handler(message, get_text_messages)


def start_process(message):
    bot.send_chat_action(my_id, 'typing')
    try:
        os.startfile(r'' + message.text)
        bot.send_message(my_id, f"Файл по пути \"{message.text}\" запустился", reply_markup=menu_keyboard)
        bot.register_next_step_handler(message, get_text_messages)
    except:
        bot.send_message(my_id, "Ошибка! Указан неверный файл", reply_markup=menu_keyboard)
        bot.register_next_step_handler(message, get_text_messages)


def downloadfile_process(message):
    bot.send_chat_action(my_id, 'typing')
    try:
        file_path = message.text
        if os.path.exists(file_path):
            bot.send_message(my_id, "Файл загружается, подождите...")
            bot.send_chat_action(my_id, 'upload_document')
            file_doc = open(file_path, 'rb')
            bot.send_document(my_id, file_doc)
            bot.register_next_step_handler(message, get_text_messages)
        else:
            bot.send_message(my_id, "Файл не найден или указан неверный путь (ПР.: C:\\Documents\\File.doc)")
            bot.register_next_step_handler(message, get_text_messages)
    except:
        bot.send_message(my_id, "Ошибка! Файл не найден или указан неверный путь (ПР.: C:\\Documents\\File.doc)")
        bot.register_next_step_handler(message, get_text_messages)


def is_digit(string):
    if string.isdigit():
        return True
    else:
        try:
            float(string)
            return True
        except ValueError:
            return False


while True:
    try:
        bot.polling(none_stop=True, interval=0, timeout=20)
    except Exception as E:
        print(E.args)
        time.sleep(2)
