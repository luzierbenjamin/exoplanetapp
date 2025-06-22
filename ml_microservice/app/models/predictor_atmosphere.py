import numpy as np
from sklearn.tree import DecisionTreeClassifier

# Dummy dataset
X = np.array([
    [1.0, 1.0, 365, 0],    # Rocky → Nitrogen-Oxygen
    [3.0, 2.5, 4000, 2],   # Gas Giant → Hydrogen-Helium
    [0.9, 0.8, 1000, 1],   # Ice → CO2-rich
    [2.5, 2.2, 2800, 2],   # Gas Giant
    [1.2, 1.1, 300, 0],    # Rocky
    [0.3, 0.3, 10, 0],     # Small Rocky → No Atmosphere
    [0.6, 0.5, 100, 1],    # Small Ice → No Atmosphere
])

y = np.array([
    0,  # Nitrogen-Oxygen
    1,  # Hydrogen-Helium
    2,  # CO2-rich
    1,  # Hydrogen-Helium
    0,  # Nitrogen-Oxygen
    3,  # No Atmosphere
    3,  # No Atmosphere
])
planet_type_map = { "Rocky": 0, "Ice": 1, "Gas Giant": 2}
atmosphere_label_map = {
    0: "Nitrogen-Oxygen",
    1: "Hydrogen-Helium",
    2: "Carbon Dioxide",
    3: "No Atmosphere"
}

model = DecisionTreeClassifier()
model.fit(X, y)

def predict_atmosphere(mass: float, radius: float, orbital_period: float, planet_type: str) -> str:
    planet_type_encoded = planet_type_map.get(planet_type, 0)
    input_data = np.array([[mass, radius, orbital_period, planet_type_encoded]])
    prediction = model.predict(input_data)[0]
    return atmosphere_label_map[prediction]