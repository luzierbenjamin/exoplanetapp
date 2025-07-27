import numpy as np
from sklearn.linear_model import LogisticRegression

# Dummy training data
X = np.array([
    [1.0, 1.0, 365],     # Rocky
    [0.5, 0.9, 100],
    [3.0, 2.5, 4000],    # Gas Giant
    [2.8, 2.2, 3000],
    [0.8, 0.7, 800],     # Ice
])

y = np.array([0, 0, 2, 2, 1]) 

model = LogisticRegression()
model.fit(X, y)

label_map = {
    0: "Rocky",
    1: "Ice",
    2: "Gas Giant"
}

def predict_planet_type(mass: float, radius: float, orbital_period: float) -> str:
    input_data = np.array([[mass, radius, orbital_period]])
    prediction = model.predict(input_data)[0]
    return label_map[prediction]