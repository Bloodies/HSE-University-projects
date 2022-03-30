import os
import tensorflow
import numpy as np
import openpyxl
import time
import pandas as pd
from tensorflow import keras as keras
import matplotlib.pyplot as plt
from keras import layers


df_train = pd.read_excel("datasetD1.xlsx", sheet_name="DATA")
df_val = pd.read_excel("datasetD1.xlsx", sheet_name="TEST")
df_test = pd.read_excel("datasetD1.xlsx", sheet_name="TEST")

df_train = df_train.drop(0, axis=0)
df_val = df_val.drop(0, axis=0)
df_test = df_test.drop(0, axis=0)
print(df_train)
print(type(df_train))

train = df_train.values
val = df_val.values
test = df_test.values
print(train)
print(type(train))

x, y = train[:, 0:11], train[:, 11]
x_v, y_v = val[:, 0:11], val[:, 11]
x_t, y_t = test[:, 0:11], test[:, 11]
print(x)
print(y)


def create_model(input_shape):
    model = keras.models.Sequential()
    model.add(keras.Input(shape=(input_shape,)))
    model.add(keras.layers.Dense(16, activation='relu'))
    model.add(keras.layers.Dense(16, activation='relu'))
    model.add(keras.layers.Dense(1, activation='sigmoid'))
    return model


model = create_model(11)
model.summary()

best_model_cl = keras.callbacks.ModelCheckpoint(
    "model.pth",
    monitor="val_loss",
    verbose=0,
    save_best_only=True,
    save_weights_only=True
)

model.compile(
    optimizer=keras.optimizers.Adam(),
    loss=keras.losses.BinaryCrossentropy(),
    metrics=[keras.metrics.BinaryAccuracy()]
)

BATCH_SIZE=10
LR=1e-3
EPOCHS=1000

model.fit(x=x.astype("float"),
          y=y.astype("float"),
          batch_size=BATCH_SIZE,
          epochs=EPOCHS,
          validation_data=(x_v.astype("float"), y_v.astype("float")),
          callbacks=[best_model_cl])

plt.figure(figsize=(8, 6))
plt.plot(range(1, EPOCHS + 1), model.history.history["loss"], label="Loss")
plt.plot(range(1, EPOCHS + 1), model.history.history["val_loss"], label="ValLoss")
plt.legend()
plt.show()

model.load_weights("model.pth")


pred = model.predict(x_t.astype(float))

print(f"Test:\nPrediction: {(pred > 0.5).reshape(-1)}\nGround truth: {y_t.astype(bool)}")