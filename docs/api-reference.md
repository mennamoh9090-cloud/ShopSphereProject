# REST API Reference

The **ShopSphere.API** service provides a RESTful interface for catalog browsing, category management, order placement, and administrative statistics.

---

## 1. Base URL & Interactive Documentation

| Environment | Base URL | Interactive Docs (Swagger) |
| :--- | :--- | :--- |
| **Local Development (HTTP)** | `http://localhost:5062` | `http://localhost:5062/swagger` |
| **Local Development (HTTPS)** | `https://localhost:7140` | `https://localhost:7140/swagger` |

> [!NOTE]
> The MVC web application communicates with the API using `http://localhost:5062/api/` as configured in `ShopSphere.MVC/Services/ApiService.cs`.

---

## 2. Products Endpoints

### `GET /api/products`
Retrieves all products, or filters products by keyword or category.

**Query Parameters:**
| Parameter | Type | Required | Description |
| :--- | :--- | :--- | :--- |
| `search` | `string` | No | Search query matched against product names. |
| `categoryId` | `integer` | No | Filter products belonging to a specific category. |

**Priority Rule:**
* If `search` is provided, the API executes a full-text search.
* If `categoryId` is provided (and `search` is omitted), results are filtered by category.
* If neither parameter is supplied, all active products are returned.

**Sample Response (`200 OK`):**
```json
[
  {
    "id": 1,
    "name": "سماعة بلوتوث لاسلكية",
    "description": "سماعة عازلة للصوت ومقاومة للماء مع بطارية تدوم 24 ساعة",
    "price": 1500.00,
    "imageUrl": "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=800",
    "stock": 10,
    "categoryId": 1
  }
]
```

---

### `GET /api/products/{id}`
Retrieves a specific product by its unique identifier.

**Responses:**
* `200 OK`: Returns the `ProductDto` object.
* `404 Not Found`: Returned if no product matches the specified `id`.

---

### `POST /api/products`
Creates and persists a new product in the catalog.

**Request Body (`ProductDto`):**
```json
{
  "name": "Mechanical Keyboard RGB",
  "description": "High performance mechanical keyboard with hot-swappable switches",
  "price": 2499.99,
  "imageUrl": "https://images.unsplash.com/photo-example.jpg",
  "stock": 15,
  "categoryId": 5
}
```

**Responses:**
* `200 OK`: `{ "success": true }`
* `400 Bad Request`: Validation failure (enforced via `ProductDtoValidator`).

---

### `PUT /api/products/{id}`
Updates existing product details.

**Path Parameters:**
* `id` (`integer`): Must match `dto.Id` in the request body.

**Responses:**
* `200 OK`: `{ "success": true }`
* `400 Bad Request`: If route `id` does not match body `dto.Id` ("المعرف غير متطابق").

---

### `DELETE /api/products/{id}`
Removes a product from the database.

**Responses:**
* `200 OK`: `{ "success": true }`

---

## 3. Categories Endpoints

### `GET /api/categories`
Retrieves all categories.

**Sample Response (`200 OK`):**
```json
[
  {
    "id": 1,
    "name": "إلكترونيات",
    "description": "موبايلات، لابتوبات، وأجهزة ذكية",
    "imageUrl": "https://images.unsplash.com/photo-1498049794561-7780e7231661?w=800"
  }
]
```

---

### `GET /api/categories/{id}`
Retrieves category details by ID.

**Responses:**
* `200 OK`: Returns the category object.
* `404 Not Found`: Category not found.

---

### `POST /api/categories`
Creates a new category.

**Request Body (`CategoryDto`):**
```json
{
  "name": "Sports & Fitness",
  "description": "Sporting equipment, apparel, and outdoor accessories",
  "imageUrl": "https://images.unsplash.com/photo-sports.jpg"
}
```

**Responses:**
* `200 OK`: `{ "success": true }`

---

### `PUT /api/categories/{id}`
Updates an existing category.

**Responses:**
* `200 OK`: `{ "success": true }`
* `400 Bad Request`: If route `id` does not match body `dto.Id`.

---

### `DELETE /api/categories/{id}`
Deletes a category by ID.

**Responses:**
* `200 OK`: `{ "success": true }`

---

## 4. Orders & Checkout Endpoints

### `GET /api/orders`
Retrieves all customer orders.

**Sample Response (`200 OK`):**
```json
[
  {
    "id": 101,
    "userId": "customer@example.com",
    "customerName": "Ahmed Ali",
    "customerEmail": "customer@example.com",
    "shippingAddress": "123 Nile St, Cairo",
    "customerPhone": "01012345678",
    "totalPrice": 3120.00,
    "createdAt": "2026-10-12T17:30:00Z",
    "orderStatus": "Pending",
    "paymentStatus": "Paid",
    "stripeSessionId": "mock_a1b2c3d4e5",
    "stripePaymentIntentId": "",
    "items": [
      {
        "productId": 1,
        "productName": "سماعة بلوتوث لاسلكية",
        "price": 1500.00,
        "quantity": 2
      }
    ]
  }
]
```

---

### `GET /api/orders/{id}`
Retrieves detailed information for a single order, including line items.

**Responses:**
* `200 OK`: Returns the order DTO.
* `404 Not Found`: Order not found.

---

### `POST /api/orders/checkout`
Creates and registers a new customer order.

**Request Body (`OrderDto`):**
```json
{
  "userId": "customer@example.com",
  "customerName": "Sara Mohamed",
  "customerEmail": "customer@example.com",
  "shippingAddress": "45 Tahrir Square, Cairo",
  "customerPhone": "01098765432",
  "totalPrice": 1560.00,
  "orderStatus": "Pending",
  "paymentStatus": "Pending",
  "stripeSessionId": "COD",
  "items": [
    {
      "productId": 1,
      "price": 1500.00,
      "quantity": 1
    }
  ]
}
```

**Responses:**
* `200 OK`: Returns the newly created `OrderDto` (including generated database `id`).
* `400 Bad Request`: If the request payload is null ("بيانات الطلب فارغة").

---

### `PUT /api/orders/{id}/status`
Updates the operational status of an order (`Pending`, `Processing`, `Shipped`, `Delivered`, `Cancelled`).

**Request Body (`string`):**
```json
"Shipped"
```

**Responses:**
* `200 OK`: `{ "success": true }`

---

## 5. Dashboard Endpoints

### `GET /api/dashboard/admin`
Fetches administrative KPI metrics, totals, and the 5 most recent products.

**Sample Response (`200 OK`):**
```json
{
  "totalProducts": 12,
  "totalCategories": 5,
  "ordersCount": 8,
  "usersCount": 14,
  "revenueGrowthPercentage": 12.5,
  "totalSales": 142500.00,
  "recentProducts": [
    {
      "id": 12,
      "name": "ذراع تحكم ألعاب لاسلكي",
      "price": 1600.00,
      "imageUrl": "https://images.unsplash.com/photo-1531525645387-7f14be1bdbbd?w=800",
      "categoryId": 5,
      "category": { "id": 5, "name": "ألعاب وقيمينج" },
      "stock": 14
    }
  ]
}
```

---

### `GET /api/dashboard/users`
Returns a list of all registered users with their username and email.

**Sample Response (`200 OK`):**
```json
[
  {
    "email": "admin@shopsphere.com",
    "fullName": "admin@shopsphere.com"
  },
  {
    "email": "user@example.com",
    "fullName": "user@example.com"
  }
]
```

---

## 6. Error Handling

Unhandled exceptions are intercepted by `GlobalExceptionMiddleware` and returned as a JSON structure:

```json
{
  "statusCode": 500,
  "message": "Internal Server Error from the custom middleware.",
  "detailed": "Detailed exception description"
}
```
