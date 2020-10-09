import pandas as pd
import matplotlib.pyplot as plt


def ShowGraphics(df, column_name_x: str, column_name_f: str):
    values = [df.loc[df[column_name_f] == val] for val in df[column_name_f].unique()]
    ys = [list(item['Time']) for item in values]
    x = df[column_name_x].unique()

    for y, label in zip(ys, (f"{column_name_f}={v}" for v in df[column_name_f].unique())):
        plt.plot(x, y, label=label)

    plt.xlabel(column_name_x)
    plt.ylabel('Time ms')
    plt.legend(loc='upper left')
    plt.show()


dflinestep2 = pd.read_csv('C:\\Users\\Nikolay\\Desktop\\BenchResult\\linebenchresult.csv', sep=',')
ShowGraphics(dflinestep2, 'LineCount', 'DictLineCount')
ShowGraphics(dflinestep2, 'DictLineCount', 'LineCount')

dfwordstep2 = pd.read_csv('C:\\Users\\Nikolay\\Desktop\\BenchResult\\wordbenchresult.csv', sep=',')
ShowGraphics(dfwordstep2, 'WordCount', 'DictLineCount')
ShowGraphics(dfwordstep2, 'DictLineCount', 'WordCount')
