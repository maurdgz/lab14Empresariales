"""Pruebas HTTP sobre la aplicación publicada; solo usan datos de demostración."""
import json, sys, time
from urllib.request import Request, urlopen
from urllib.error import HTTPError, URLError
base = sys.argv[1] if len(sys.argv) > 1 else "http://localhost:5140"
def request(method, path, data=None, status=200):
    body = json.dumps(data).encode() if data is not None else None
    req = Request(base+path, body, {"Content-Type": "application/json"}, method=method)
    try:
        response = urlopen(req, timeout=10)
    except HTTPError as error:
        response = error
    assert response.status == status, (method, path, response.status, response.read())
    raw = response.read()
    return json.loads(raw) if raw else None
for attempt in range(30):
    try:
        request("GET", "/health")
        break
    except (URLError, TimeoutError):
        if attempt == 29: raise
        time.sleep(1)
schema = request("GET", "/swagger/v1/swagger.json")
assert {"get", "post"} <= schema["paths"]["/api/productos"].keys()
request("GET", "/api/productos")
id = request("POST", "/api/productos", {"nombre": "Prueba Lab14", "precio": 99}, 201)["id"]
try:
    assert request("GET", f"/api/productos/{id}")["precio"] == 99
    request("PUT", f"/api/productos/{id}", {"nombre": "Prueba editada", "precio": 125}, 204)
    assert request("GET", f"/api/productos/{id}")["nombre"] == "Prueba editada"
    request("POST", "/api/productos", {"nombre": " ", "precio": 10}, 400)
    request("POST", "/api/productos", {"nombre": "Inválido", "precio": -1}, 400)
    request("POST", "/api/productos", {"nombre": "X"*101, "precio": 10}, 400)
finally:
    request("DELETE", f"/api/productos/{id}", status=204)
request("GET", f"/api/productos/{id}", status=404)
request("PUT", f"/api/productos/{id}", {"nombre": "Ya no existe", "precio": 10}, 404)
print("OK: health, Swagger, CRUD, validaciones 400 y recursos inexistentes 404.")
