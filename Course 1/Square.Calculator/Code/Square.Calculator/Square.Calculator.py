from tkinter import *
import tkinter
import tkinter.ttk
import math


def create_widgets_in_first_frame():
    # Create the label for the frame
    first_window_label = tkinter.ttk.Label(first_frame,
                                           text='Choose Language')
    first_window_label.grid(column=2,
                            row=0,
                            pady=10,
                            padx=10,
                            sticky=(tkinter.N))

    # Create the button for the frame
    create_widgets_in_first_frame.add_img = tkinter.PhotoImage(file="1.png")
    create_widgets_in_second_frame.add_img = tkinter.PhotoImage(file="2.png")
    create_widgets_in_third_frame.add_img = tkinter.PhotoImage(file="3.png")
    create_widgets_in_fourth_frame.add_img = tkinter.PhotoImage(file="4.png")
    first_window_next_button = tkinter.Button(first_frame,
                                              text="Русский",
                                              image=create_widgets_in_first_frame.add_img,
                                              command=call_second_frame_on_top)
    first_window_next_button.grid(column=1,
                                  row=1,
                                  pady=10,
                                  padx=10)
    first_window_next_button = tkinter.Button(first_frame,
                                              text="Английский",
                                              image=create_widgets_in_second_frame.add_img,
                                              command=call_third_frame_on_top)
    first_window_next_button.grid(column=1,
                                  row=2,
                                  pady=10,
                                  padx=10)
    first_window_next_button = tkinter.Button(first_frame,
                                              text="Китайский",
                                              image=create_widgets_in_third_frame.add_img,
                                              command=call_fourth_frame_on_top)
    first_window_next_button.grid(column=3,
                                  row=1,
                                  pady=10,
                                  padx=10)
    first_window_next_button = tkinter.Button(first_frame,
                                              text="Французский",
                                              image=create_widgets_in_fourth_frame.add_img,
                                              command=call_fifth_frame_on_top)
    first_window_next_button.grid(column=3,
                                  row=2,
                                  pady=10,
                                  padx=10)
    first_window_quit_button = tkinter.Button(first_frame,
                                              text="Exit",
                                              command=quit_program)
    first_window_quit_button.grid(column=4,
                                  row=3,
                                  pady=10,
                                  padx=10)


def create_widgets_in_second_frame():
    # Create the label for the frame
    second_window_label = tkinter.ttk.Label(second_frame,
                                           text='Введите число:')
    second_window_label.grid(column=1,
                            row=0,
                            pady=10,
                            padx=10,
                            sticky=(tkinter.N))

    message = StringVar()
    entry1 = tkinter.Entry(second_frame, text='', textvariable=message, width=50,)
    entry1.grid(column=2,
                       row=0,
                       pady=10,
                       padx=10,
                       sticky=(tkinter.N))

    second_window_label = tkinter.ttk.Label(second_frame,
                                           text='Точность(<15 знаков):')
    second_window_label.grid(column=1,
                            row=1,
                            pady=10,
                            padx=10,
                            sticky=(tkinter.N))
    many = StringVar()
    message_entry = Entry(second_frame, text='', textvariable=many, width=20, )
    message_entry.grid(column=2,
                       row=1,
                       pady=10,
                       padx=10,
                       sticky=(tkinter.N))

    def func1():
        try:
            x = float(entry1.get())
            second_window_label1.config(text="{}".format(math.sqrt(x)))
        except ValueError:
            second_window_label1.config(text="Ошибка введите цифры")

    second_window_label1 = tkinter.Label(second_frame,
                                           text="Ответ:")
    second_window_label1.grid(column=2,
                            row=3,
                            pady=10,
                            padx=10,
                            sticky=(tkinter.N))

    second_window_label = tkinter.ttk.Label(second_frame,
                                           text="Ответ:")
    second_window_label.grid(column=1,
                            row=3,
                            pady=10,
                            padx=10,
                            sticky=(tkinter.N))

    # Create the button for the frame
    second_window_enter_button = tkinter.Button(second_frame, text='Решить', command=func1())
    second_window_enter_button.grid(column=2,
                                    row=2,
                                    pady=10,
                                    padx=10)
    second_window_back_button = tkinter.Button(second_frame,
                                               text="Назад",
                                               command=call_first_frame_on_top)
    second_window_back_button.grid(column=0,
                                   row=5,
                                   pady=10,
                                   padx=10)
    second_window_next_button = tkinter.Button(second_frame,
                                               text="Выход",
                                               command=quit_program)
    second_window_next_button.grid(column=5,
                                   row=5,
                                   pady=10,
                                   padx=10)


def create_widgets_in_third_frame():
    # Create the label for the frame
    third_window_label = tkinter.ttk.Label(third_frame,
                                            text='输入一个数字:')
    third_window_label.grid(column=1,
                             row=0,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))

    message = StringVar()
    entry1 = tkinter.Entry(third_frame, text='', textvariable=message, width=50, )
    entry1.grid(column=2,
                row=0,
                pady=10,
                padx=10,
                sticky=(tkinter.N))

    third_window_label = tkinter.ttk.Label(third_frame,
                                            text='准确度（<15个字符):')
    third_window_label.grid(column=1,
                             row=1,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))
    many = StringVar()
    message_entry = Entry(third_frame, text='', textvariable=many, width=20, )
    message_entry.grid(column=2,
                       row=1,
                       pady=10,
                       padx=10,
                       sticky=(tkinter.N))

    def func1():
        try:
            x = float(entry1.get())
            third_window_label1.config(text="{}".format(math.sqrt(x)))
        except ValueError:
            third_window_label1.config(text="错误输入数字")

    third_window_label1 = tkinter.Label(third_frame,
                                         text="回答:")
    third_window_label1.grid(column=2,
                              row=3,
                              pady=10,
                              padx=10,
                              sticky=(tkinter.N))

    third_window_label = tkinter.ttk.Label(third_frame,
                                            text="回答:")
    third_window_label.grid(column=1,
                             row=3,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))

    # Create the button for the frame
    third_window_enter_button = tkinter.Button(third_frame, text='解决', command=func1())
    third_window_enter_button.grid(column=2,
                                    row=2,
                                    pady=10,
                                    padx=10)
    # Create the button for the frame
    third_window_back_button = tkinter.Button(third_frame,
                                              text="向后",
                                              command=call_first_frame_on_top)
    third_window_back_button.grid(column=0,
                                  row=5,
                                  pady=10,
                                  padx=10,
                                  sticky=(tkinter.N))
    third_window_quit_button = tkinter.Button(third_frame,
                                              text="输出",
                                              command = quit_program)
    third_window_quit_button.grid(column=5,
                                  row=5,
                                  pady=10,
                                  padx=10,
                                  sticky=(tkinter.N))


def create_widgets_in_fourth_frame():
    # Create the label for the frame
    fourth_window_label = tkinter.ttk.Label(fourth_frame,
                                            text='Enter a number:')
    fourth_window_label.grid(column=1,
                             row=0,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))

    message = StringVar()
    entry1 = tkinter.Entry(fourth_frame, text='', textvariable=message, width=50, )
    entry1.grid(column=2,
                row=0,
                pady=10,
                padx=10,
                sticky=(tkinter.N))

    fourth_window_label = tkinter.ttk.Label(fourth_frame,
                                            text='Accuracy(<15 numbers):')
    fourth_window_label.grid(column=1,
                             row=1,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))
    many = StringVar()
    message_entry = Entry(fourth_frame, text='', textvariable=many, width=20, )
    message_entry.grid(column=2,
                       row=1,
                       pady=10,
                       padx=10,
                       sticky=(tkinter.N))

    def func1():
        try:
            x = float(entry1.get())
            fourth_window_label1.config(text="{}".format(math.sqrt(x)))
        except ValueError:
            fourth_window_label1.config(text="Error enter numbers")

    fourth_window_label1 = tkinter.Label(fourth_frame,
                                         text="Answer:")
    fourth_window_label1.grid(column=2,
                              row=3,
                              pady=10,
                              padx=10,
                              sticky=(tkinter.N))

    fourth_window_label = tkinter.ttk.Label(fourth_frame,
                                            text="Answer:")
    fourth_window_label.grid(column=1,
                             row=3,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))

    # Create the button for the frame
    fourth_window_enter_button = tkinter.Button(fourth_frame, text='Solve', command=func1())
    fourth_window_enter_button.grid(column=2,
                                    row=2,
                                    pady=10,
                                    padx=10)
    # Create the button for the frame
    fourth_window_back_button = tkinter.Button(fourth_frame,
                                               text="Back",
                                               command=call_first_frame_on_top)
    fourth_window_back_button.grid(column=0,
                                   row=5,
                                   pady=10,
                                   padx=10,
                                   sticky=(tkinter.N))
    fourth_window_quit_button = tkinter.Button(fourth_frame,
                                               text="Exit",
                                               command=quit_program)
    fourth_window_quit_button.grid(column=5,
                                   row=5,
                                   pady=10,
                                   padx=10,
                                   sticky=(tkinter.N))


def create_widgets_in_fifth_frame():
    # Create the label for the frame
    fifth_window_label = tkinter.ttk.Label(fifth_frame,
                                            text='Entrez un nombre:')
    fifth_window_label.grid(column=1,
                             row=0,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))

    message = StringVar()
    entry1 = tkinter.Entry(fifth_frame, text='', textvariable=message, width=50, )
    entry1.grid(column=2,
                row=0,
                pady=10,
                padx=10,
                sticky=(tkinter.N))

    fifth_window_label = tkinter.ttk.Label(fifth_frame,
                                            text='Précision(<15 caractères):')
    fifth_window_label.grid(column=1,
                             row=1,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))
    many = StringVar()
    message_entry = Entry(fifth_frame, text='', textvariable=many, width=20, )
    message_entry.grid(column=2,
                       row=1,
                       pady=10,
                       padx=10,
                       sticky=(tkinter.N))

    def func1():
        try:
            x = float(entry1.get())
            fifth_window_label1.config(text="{}".format(math.sqrt(x)))
        except ValueError:
            fifth_window_label1.config(text="Erreur entrez les chiffres")

    fifth_window_label1 = tkinter.Label(fifth_frame,
                                         text="Réponse:")
    fifth_window_label1.grid(column=2,
                              row=3,
                              pady=10,
                              padx=10,
                              sticky=(tkinter.N))

    fifth_window_label = tkinter.ttk.Label(fifth_frame,
                                            text="Réponse:")
    fifth_window_label.grid(column=1,
                             row=3,
                             pady=10,
                             padx=10,
                             sticky=(tkinter.N))

    # Create the button for the frame
    fifth_window_enter_button = tkinter.Button(fifth_frame, text='Résoudre', command=func1())
    fifth_window_enter_button.grid(column=2,
                                    row=2,
                                    pady=10,
                                    padx=10)
    # Create the button for the frame
    fifth_window_back_button = tkinter.Button(fifth_frame,
                                              text="Retourner",
                                              command=call_first_frame_on_top)
    fifth_window_back_button.grid(column=0,
                                  row=5,
                                  pady=10,
                                  padx=10,
                                  sticky=(tkinter.N))
    fifth_window_quit_button = tkinter.Button(fifth_frame,
                                              text="Sortir",
                                              command=quit_program)
    fifth_window_quit_button.grid(column=5,
                                  row=5,
                                  pady=10,
                                  padx=10,
                                  sticky=(tkinter.N))


def call_first_frame_on_top():
    # This function can be called only from the second window.
    # Hide the second window and show the first window.
    second_frame.place_forget()
    third_frame.place_forget()
    fourth_frame.place_forget()
    fifth_frame.place_forget()
    first_frame.place(relx=0.1, rely=0.1)


def call_second_frame_on_top():
    # This function can be called from the first and third windows.
    # Hide the first and third windows and show the second window.
    first_frame.place_forget()
    second_frame.place(relx=0.1, rely=0.1)


def call_third_frame_on_top():
    # This function can only be called from the second window.
    # Hide the second window and show the third window.
    first_frame.place_forget()
    third_frame.place(relx=0.1, rely=0.1)


def call_fourth_frame_on_top():
    # This function can only be called from the second window.
    # Hide the second window and show the third window.
    first_frame.place_forget()
    fourth_frame.place(relx=0.1, rely=0.1)


def call_fifth_frame_on_top():
    # This function can only be called from the second window.
    # Hide the second window and show the third window.
    first_frame.place_forget()
    fifth_frame.place(relx=0.1, rely=0.1)


def quit_program():
    root_window.destroy()




###############################
# Main program starts here :) #
###############################

# Create the root GUI window.
root_window = tkinter.Tk()

root_window.title("Калькулятор квадратов")
root_window.geometry("700x400")
root_window.resizable(False, False)


# Create frames inside the root window to hold other GUI elements. All frames must be created in the main program, otherwise they are not accessible in functions.
first_frame = tkinter.ttk.Frame(root_window, width=750, height=450+300+200)
first_frame.place(relx=0.1, rely=0.1)

second_frame = tkinter.ttk.Frame(root_window, width=750, height=450+300+200)
second_frame.place(relx=0.1, rely=0.1)

third_frame = tkinter.ttk.Frame(root_window, width=750, height=450+300+200)
third_frame.place(relx=0.1, rely=0.1)

fourth_frame = tkinter.ttk.Frame(root_window, width=750, height=450+300+200)
fourth_frame.place(relx=0.1, rely=0.1)

fifth_frame = tkinter.ttk.Frame(root_window, width=750, height=450+300+200)
fifth_frame.place(relx=0.1, rely=0.1)

# Create all widgets to all frames
create_widgets_in_first_frame()
create_widgets_in_second_frame()
create_widgets_in_third_frame()
create_widgets_in_fourth_frame()
create_widgets_in_fifth_frame()


# Hide all frames in reverse order, but leave first frame visible (unhidden).
second_frame.place_forget()
third_frame.place_forget()
fourth_frame.place_forget()
fifth_frame.place_forget()

# Start tkinter event - loop
root_window.mainloop()


Application.EnableVisualStyles()
Application.SetCompatibleTextRenderingDefault(False)

form = MyForm()
Application.Run(form)
