import numpy as np
from sklearn.linear_model import LogisticRegression

X = np.array([[1, 1], [0.5, 0.8], [3, 2.5], [0.9, 1.2]])
y = np.array([1, 0, 2, 1])  # 0 = Rocky, 1 = Gas, 2 = Ice

model = LogisticRegression()
model.fit(X, y)

def predict_type(mass: float, radius: float, orbitalPeriod: float) -> str:
    types = {0: "Rocky", 1: "Gas", 2: "Ice"}
    prediction = model.predict([[mass, radius, orbitalPeriod]])[0]
    return types[prediction]
