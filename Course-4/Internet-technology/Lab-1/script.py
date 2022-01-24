import re
import roman

'''
1) \b[1-9][0-9]{5}\b
test: 123456 1234567 12345 234567

2) (?<!:)[0-9a-fA-F]{2}(:[0-9a-fA-F]{2}){5}(?!:)
test: 01:32:54:67:89:AB 01:33:47:65:89:ab:cd aE:dC:cA:56:76:54 01:23:45:67:89:Az 

3) #[a-fA-F0-9]{6}\b
test: #FFFFFF #FF3421 232323 f#fddee #00ff00 #fd2  

4) (?<!\.)\b((([2](([0-4][0-9])|([5][0-5])))|([1][0-9][0-9])|([0-9][0-9])|([0-9]))\.){3}(([2](([0-4][0-9])|([5][0-5])))|([1][0-9][0-9])|([0-9][0-9])|([0-9]))\b(?!\.)
test: 111.0.12.244 123.4.2333.34 1.4.3 123.155.155.155 255.255.255.255

5) \b(?=.*[a-z])(?=.*[A-Z])(?=.*[_]?)(?=.*\d)[a-zA-Z\d_]{8,}\b
test: C00l_Pass Cool_pass SupperPas1 C00l asdasdasd ASDasdasdasd 

6) ([0-2][0-4]{1,2}\:([0-5][0-9]|6[0]){1,2}\:([0-5][0-9]|6[0]){1,2})
test: 24:60:60 00:00:00 25:12:12 12:70:21 

7) [А-ЯЁ][а-яё]*([-][А-ЯЁ][а-яё]*)?\s+([А-Я]\.){2}|([А-Я]\.){2}\s+[А-ЯЁ][а-яё]*([-][А-ЯЁ][а-яё]*)?
test: Иванов И.И. ы.ф. Ыфыч Е.С. Чепоков Иосиф-Пригожин У.У.

8) (?<n11>(".*[!?]")(?=-))|(?<n12>(".*")(?=,-))|(?<n21>((?<=:)".*([?!]|(\.\.\.))"))|(?<n22>((?<=:)".*)"(?=\.))|(?<n31>(".*)(?:,-.*,-)([а-я].*"))|(?<n32>(".*)(?:,-.*\.-)([А-Я].*"))|(?<n33>(".*)(?:,-.*:-)([А-Я].*"))
'''

regex = r'\b(?=[IVXLMCD])M{0,3}(?:CM|CD|D?C{0,3})(?:XC|XL|L?X{0,3})(?:IX|IV|V?I{0,3})\b'
open('output.txt', 'w').close()


def roman_value(c):
    if c == 'M':
        return 1000
    elif c == 'D':
        return 500
    elif c == 'C':
        return 100
    elif c == 'L':
        return 50
    elif c == 'X':
        return 10
    elif c == 'V':
        return 5
    elif c == 'I':
        return 1
    return -1


def romanToInt(s):
    if s is None or len(s) == 0:
        return 0

    total = 0
    current = s[0]
    run = roman_value(current)

    for i in range(1, len(s)):
        next_item = s[i]
        value = roman_value(s[i])
        if next_item == current:
            run += value
        else:
            if value < roman_value(current):
                total += run
            else:
                total -= run
            run = roman_value(next_item)

        current = next_item

    return total + run


def repl(m):
    if m.group(0) != "":
        return str(roman.fromRoman(m.group(0)))


with open("input.txt", "r", encoding='utf-8') as file:
    for line in file:
        line = re.sub(regex, repl, line)
        print(line.replace('\n', ''))
        with open("output.txt", "a", encoding='utf-8') as output:
            output.write(line)
