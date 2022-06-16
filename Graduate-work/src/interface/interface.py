# -*- coding: utf-8 -*-

import numpy
import sys
import tensorflow
import time
from PyQt5 import QtCore, QtGui, QtWidgets
from keras.models import load_model

model = load_model('NeuroSet')


class Ui_MainWindow(object):
    def __init__(self):
        self._mutex = QtCore.QMutex()

    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(793, 367)
        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.gridLayoutWidget = QtWidgets.QWidget(self.centralwidget)
        self.gridLayoutWidget.setGeometry(QtCore.QRect(10, 10, 471, 351))
        self.gridLayoutWidget.setObjectName("gridLayoutWidget")
        self.gridLayout = QtWidgets.QGridLayout(self.gridLayoutWidget)
        self.gridLayout.setContentsMargins(0, 0, 0, 0)
        self.gridLayout.setObjectName("gridLayout")
        self.lblOscars = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblOscars.setObjectName("lblOscars")
        self.gridLayout.addWidget(self.lblOscars, 11, 0, 1, 1)
        self.lblWrAwards = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblWrAwards.setObjectName("lblWrAwards")
        self.gridLayout.addWidget(self.lblWrAwards, 9, 0, 1, 1)
        self.lblStAwards = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblStAwards.setObjectName("lblStAwards")
        self.gridLayout.addWidget(self.lblStAwards, 10, 0, 1, 1)
        self.lblDuration = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblDuration.setObjectName("lblDuration")
        self.gridLayout.addWidget(self.lblDuration, 1, 0, 1, 1)
        self.lblMpaa = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblMpaa.setObjectName("lblMpaa")
        self.gridLayout.addWidget(self.lblMpaa, 3, 0, 1, 1)
        self.lblGenre = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblGenre.setObjectName("lblGenre")
        self.gridLayout.addWidget(self.lblGenre, 2, 0, 1, 1)
        self.lblDirAwards = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblDirAwards.setObjectName("lblDirAwards")
        self.gridLayout.addWidget(self.lblDirAwards, 8, 0, 1, 1)
        self.lblBudget = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblBudget.setMinimumSize(QtCore.QSize(300, 0))
        self.lblBudget.setBaseSize(QtCore.QSize(0, 0))
        self.lblBudget.setObjectName("lblBudget")
        self.gridLayout.addWidget(self.lblBudget, 0, 0, 1, 1)
        self.lblFranchise = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblFranchise.setTextFormat(QtCore.Qt.AutoText)
        self.lblFranchise.setObjectName("lblFranchise")
        self.gridLayout.addWidget(self.lblFranchise, 4, 0, 1, 1)
        self.lblSeason = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblSeason.setObjectName("lblSeason")
        self.gridLayout.addWidget(self.lblSeason, 5, 0, 1, 1)
        self.lblHoliday = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblHoliday.setObjectName("lblHoliday")
        self.gridLayout.addWidget(self.lblHoliday, 6, 0, 1, 1)
        self.lblDirRating = QtWidgets.QLabel(self.gridLayoutWidget)
        self.lblDirRating.setObjectName("lblDirRating")
        self.gridLayout.addWidget(self.lblDirRating, 7, 0, 1, 1)
        self.tbBudget = QtWidgets.QLineEdit(self.gridLayoutWidget)
        self.tbBudget.setObjectName("tbBudget")
        self.gridLayout.addWidget(self.tbBudget, 0, 1, 1, 1)
        self.tbDuration = QtWidgets.QLineEdit(self.gridLayoutWidget)
        self.tbDuration.setObjectName("tbDuration")
        self.gridLayout.addWidget(self.tbDuration, 1, 1, 1, 1)
        self.tbOscars = QtWidgets.QLineEdit(self.gridLayoutWidget)
        self.tbOscars.setObjectName("tbOscars")
        self.gridLayout.addWidget(self.tbOscars, 11, 1, 1, 1)
        self.cbGenre = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbGenre.setObjectName("cbGenre")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.cbGenre.addItem("")
        self.gridLayout.addWidget(self.cbGenre, 2, 1, 1, 1)
        self.cbMpaa = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbMpaa.setObjectName("cbMpaa")
        self.cbMpaa.addItem("")
        self.cbMpaa.addItem("")
        self.cbMpaa.addItem("")
        self.cbMpaa.addItem("")
        self.cbMpaa.addItem("")
        self.gridLayout.addWidget(self.cbMpaa, 3, 1, 1, 1)
        self.cbFranchise = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbFranchise.setObjectName("cbFranchise")
        self.cbFranchise.addItem("")
        self.cbFranchise.addItem("")
        self.gridLayout.addWidget(self.cbFranchise, 4, 1, 1, 1)
        self.cbSeason = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbSeason.setObjectName("cbSeason")
        self.cbSeason.addItem("")
        self.cbSeason.addItem("")
        self.cbSeason.addItem("")
        self.cbSeason.addItem("")
        self.gridLayout.addWidget(self.cbSeason, 5, 1, 1, 1)
        self.cbHoliday = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbHoliday.setObjectName("cbHoliday")
        self.cbHoliday.addItem("")
        self.cbHoliday.addItem("")
        self.gridLayout.addWidget(self.cbHoliday, 6, 1, 1, 1)
        self.cbDirRating = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbDirRating.setObjectName("cbDirRating")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.cbDirRating.addItem("")
        self.gridLayout.addWidget(self.cbDirRating, 7, 1, 1, 1)
        self.cbDirAwards = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbDirAwards.setObjectName("cbDirAwards")
        self.cbDirAwards.addItem("")
        self.cbDirAwards.addItem("")
        self.gridLayout.addWidget(self.cbDirAwards, 8, 1, 1, 1)
        self.cbWrAwards = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbWrAwards.setObjectName("cbWrAwards")
        self.cbWrAwards.addItem("")
        self.cbWrAwards.addItem("")
        self.gridLayout.addWidget(self.cbWrAwards, 9, 1, 1, 1)
        self.cbStAwards = QtWidgets.QComboBox(self.gridLayoutWidget)
        self.cbStAwards.setObjectName("cbStAwards")
        self.cbStAwards.addItem("")
        self.cbStAwards.addItem("")
        self.gridLayout.addWidget(self.cbStAwards, 10, 1, 1, 1)
        self.btnPredict = QtWidgets.QPushButton(self.centralwidget)
        self.btnPredict.setGeometry(QtCore.QRect(490, 10, 75, 23))
        self.btnPredict.setObjectName("btnPredict")
        self.lblOutput = QtWidgets.QLabel(self.centralwidget)
        self.lblOutput.setGeometry(QtCore.QRect(490, 40, 291, 311))
        self.lblOutput.setText("")
        self.lblOutput.setAlignment(QtCore.Qt.AlignLeading | QtCore.Qt.AlignLeft | QtCore.Qt.AlignTop)
        self.lblOutput.setObjectName("lblOutput")
        self.lblWait = QtWidgets.QLabel(self.centralwidget)
        self.lblWait.setGeometry(QtCore.QRect(570, 10, 131, 21))
        self.lblWait.setText("")
        self.lblWait.setObjectName("lblWait")
        MainWindow.setCentralWidget(self.centralwidget)

        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

        self.callPredict()

    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "Предсказание рентабельности кинокартины"))
        self.lblOscars.setText(_translate("MainWindow", "Суммарное количество оскаров у съемочной группы"))
        self.lblWrAwards.setText(_translate("MainWindow",
                                            "<html><head/><body><p>Имеются ли у сценариста престижные награды<br>(«Оскар», «Золотой глобус», SAAG, Critics’ Choice Awards)</br></body></html>"))
        self.lblStAwards.setText(_translate("MainWindow",
                                            "Имеются ли у актерова престижные награды<br>(«Оскар», «Золотой глобус», SAAG, Critics’ Choice Awards)</br>"))
        self.lblDuration.setText(_translate("MainWindow", "Продолжительность фильма (В минутах)"))
        self.lblMpaa.setText(_translate("MainWindow", "Возрастное ограничение фильма"))
        self.lblGenre.setText(_translate("MainWindow", "Основной жанр фильма"))
        self.lblDirAwards.setText(_translate("MainWindow",
                                             "Имеются ли у режиссера престижные награды<br>(«Оскар», «Золотой глобус», SAAG, Critics’ Choice Awards)</br>"))
        self.lblBudget.setText(_translate("MainWindow", "Бюджет фильма (Доллары США)"))
        self.lblFranchise.setText(_translate("MainWindow",
                                             "<html><head/><body><p>Является ли фильм продолжением кинофраншизы<br>(Является следуещей частью уже вышедшего)</br></p></body></html>"))
        self.lblSeason.setText(_translate("MainWindow", "Планируемый сезон выхода"))
        self.lblHoliday.setText(_translate("MainWindow", "Планируется ли выход в период высокой посещаемости"))
        self.lblDirRating.setText(_translate("MainWindow", "Рейтинг предыдущих работ режиссера"))
        self.cbGenre.setItemText(0, _translate("MainWindow", "Боевик (Action)"))
        self.cbGenre.setItemText(1, _translate("MainWindow", "Приключение (Adventure)"))
        self.cbGenre.setItemText(2, _translate("MainWindow", "Драма"))
        self.cbGenre.setItemText(3, _translate("MainWindow", "Комедия"))
        self.cbGenre.setItemText(4, _translate("MainWindow", "Криминальный фильм"))
        self.cbGenre.setItemText(5, _translate("MainWindow", "Мистика"))
        self.cbGenre.setItemText(6, _translate("MainWindow", "Хоррор"))
        self.cbGenre.setItemText(7, _translate("MainWindow", "Исторический"))
        self.cbGenre.setItemText(8, _translate("MainWindow", "Документальный"))
        self.cbGenre.setItemText(9, _translate("MainWindow", "Анимация"))
        self.cbGenre.setItemText(10, _translate("MainWindow", "Фантастика"))
        self.cbGenre.setItemText(11, _translate("MainWindow", "Триллер"))
        self.cbGenre.setItemText(12, _translate("MainWindow", "Мюзикл"))
        self.cbMpaa.setItemText(0, _translate("MainWindow", "0+"))
        self.cbMpaa.setItemText(1, _translate("MainWindow", "6+"))
        self.cbMpaa.setItemText(2, _translate("MainWindow", "12+"))
        self.cbMpaa.setItemText(3, _translate("MainWindow", "16+"))
        self.cbMpaa.setItemText(4, _translate("MainWindow", "18+"))
        self.cbFranchise.setItemText(0, _translate("MainWindow", "Нет"))
        self.cbFranchise.setItemText(1, _translate("MainWindow", "Да"))
        self.cbSeason.setItemText(0, _translate("MainWindow", "Зима"))
        self.cbSeason.setItemText(1, _translate("MainWindow", "Весна"))
        self.cbSeason.setItemText(2, _translate("MainWindow", "Лето"))
        self.cbSeason.setItemText(3, _translate("MainWindow", "Осень"))
        self.cbHoliday.setItemText(0, _translate("MainWindow", "Нет"))
        self.cbHoliday.setItemText(1, _translate("MainWindow", "Да"))
        self.cbDirRating.setItemText(0, _translate("MainWindow", "0"))
        self.cbDirRating.setItemText(1, _translate("MainWindow", "1"))
        self.cbDirRating.setItemText(2, _translate("MainWindow", "2"))
        self.cbDirRating.setItemText(3, _translate("MainWindow", "3"))
        self.cbDirRating.setItemText(4, _translate("MainWindow", "4"))
        self.cbDirRating.setItemText(5, _translate("MainWindow", "5"))
        self.cbDirRating.setItemText(6, _translate("MainWindow", "6"))
        self.cbDirRating.setItemText(7, _translate("MainWindow", "7"))
        self.cbDirRating.setItemText(8, _translate("MainWindow", "8"))
        self.cbDirRating.setItemText(9, _translate("MainWindow", "9"))
        self.cbDirRating.setItemText(10, _translate("MainWindow", "10"))
        self.cbDirAwards.setItemText(0, _translate("MainWindow", "Нет"))
        self.cbDirAwards.setItemText(1, _translate("MainWindow", "Да"))
        self.cbWrAwards.setItemText(0, _translate("MainWindow", "Нет"))
        self.cbWrAwards.setItemText(1, _translate("MainWindow", "Да"))
        self.cbStAwards.setItemText(0, _translate("MainWindow", "Нет"))
        self.cbStAwards.setItemText(1, _translate("MainWindow", "Да"))
        self.btnPredict.setText(_translate("MainWindow", "Рассчитать"))

    def callPredict(self):
        self.btnPredict.clicked.connect(lambda: self.write_data([self.tbBudget.text(),
                                                                 self.tbDuration.text(),
                                                                 self.cbGenre.currentText(),
                                                                 self.cbMpaa.currentText(),
                                                                 self.cbFranchise.currentText(),
                                                                 self.cbSeason.currentText(),
                                                                 self.cbHoliday.currentText(),
                                                                 self.cbDirRating.currentText(),
                                                                 self.cbDirAwards.currentText(),
                                                                 self.cbWrAwards.currentText(),
                                                                 self.cbStAwards.currentText(),
                                                                 self.tbOscars.text()]))

    def write_data(self, data):
        self.lblOutput.setText('')

        genre_format = {'Боевик (Action)': 1, 'Приключение (Adventure)': 2,
                        'Драма': 3, 'Комедия': 4, 'Криминальный фильм': 5,
                        'Мистика': 6, 'Хоррор': 7, 'Исторический': 8,
                        'Документальный': 9, 'Анимация': 10, 'Фантастика': 11,
                        'Триллер': 12, 'Мюзикл': 13}
        agelimit_format = {'0+': 1, '6+': 2, '12+': 3, '16+': 4, '18+': 5}
        yesno_format = {'Нет': 0, 'Да': 1}
        season_format = {'Зима': 4, 'Весна': 1, 'Лето': 2, 'Осень': 3}

        data1, data2, data3 = False, False, False

        if str(data[0]).isdigit():
            if 100000 <= int(data[0]):
                data1 = True

        if str(data[1]).isdigit():
            if 50 <= int(data[1]) <= 400:
                data2 = True

        if str(data[11]).isdigit():
            if int(data[11]) <= 30:
                data3 = True

        if (data1 and data2 and data3) is not True:
            if data1 is not True:
                self.lblOutput.setText(self.lblOutput.text() +
                                       '\nВведите бюджет в формате "1000000"\n(минимальное значение 100000)\n')
            if data2 is not True:
                self.lblOutput.setText(self.lblOutput.text() +
                                       '\nВведите длительность в числовом формате\n(в диапазоне от 50 до 400)\n')
            if data3 is not True:
                self.lblOutput.setText(self.lblOutput.text() +
                                       '\nВведите количество оскаров в числовом формате\n(количество штук < 30)\n')
        else:
            self.lblOutput.setText('')
            budget = int(data[0])
            duration = int(data[1])
            genre = genre_format[data[2]]
            mpaa = agelimit_format[data[3]]
            franchise = yesno_format[data[4]]
            season = season_format[data[5]]
            holiday = yesno_format[data[6]]
            dirrating = int(data[7])
            dirawards = yesno_format[data[8]]
            wrawards = yesno_format[data[9]]
            stawards = yesno_format[data[10]]
            oscars = int(data[11])

            self.lblWait.setText('Loading.')
            time.sleep(1)

            # Формирование датасета для предсказания
            output = numpy.array([[mpaa, duration, season, holiday, dirawards, wrawards,
                                   stawards, oscars, genre, franchise, budget],
                                  [mpaa, duration, season, holiday, dirawards, wrawards,
                                   stawards, oscars, genre, franchise, int(budget - ((budget/100)*5))],
                                  [mpaa, duration, season, holiday, dirawards, wrawards,
                                   stawards, oscars, genre, franchise, int(budget - ((budget/100)*10))],
                                  [mpaa, duration, season, holiday, dirawards, wrawards,
                                   stawards, oscars, genre, franchise, int(budget + ((budget/100)*5))],
                                  [mpaa, duration, season, holiday, dirawards, wrawards,
                                   stawards, oscars, genre, franchise, int(budget + ((budget/100)*10))],
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
            self.lblWait.setText('')

            result = 0
            pred = round((predictions[0, 0]) / 10) * 10
            if pred <= 10:
                result = 500000
            elif 11 < pred <= 20:
                result = 1000000
            elif 21 < pred <= 30:
                result = 2500000
            elif 31 < pred <= 40:
                result = 5000000
            elif 41 < pred <= 50:
                result = 7500000
            elif 51 < pred <= 60:
                result = 10000000
            elif 61 < pred <= 70:
                result = 15000000
            elif 71 < pred <= 80:
                result = 20000000
            elif 81 < pred <= 90:
                result = 30000000
            elif 91 <= pred:
                result = 40000000
            self.lblOutput.setText(f'Прогнозируемые кассовые сборы: ~${result}')


if __name__ == "__main__":
    app = QtWidgets.QApplication(sys.argv)
    MainWindow = QtWidgets.QMainWindow()
    ui = Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.show()
    sys.exit(app.exec_())
