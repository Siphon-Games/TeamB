# Interactable System for Unity

This system supports interactions in Unity using **Trigger Colliders** and **Raycasting**. Below are the instructions for creating interactables and interactors, as well as how to implement custom interaction behavior.

---

## Interactables & Interactors

### Creating Interactables

To define an interactable object in your game:

- Any class representing an interactable game object should implement the `IInteractable` interface.
- Interactors are required to invoke the interaction(s) of an interactable object.
- **For Raycasting**: The interactable must have a **Collider** (2D or 3D).

### Creating Interactors

To define an agent that can interact with interactables:

- Any class representing an agent (or player) that interacts with interactables should implement the `IInteractor` interface.

#### Raycasting Interactors
- The interactor continually fires a **ray** in its forward direction.
- When an `IInteractable` object with a **Collider** is detected, the interactor can invoke an interaction.

##### Customization Options for Raycasting:
- **RayRange**: Distance the ray travels.
- **LayerMask**: Defines which layers are valid for raycasting (e.g., specific objects or environment layers).

#### TriggerCollider2D & 3D Interactors
- The interactor uses `OnTriggerEnter2D` (for 2D objects) or `OnTriggerEnter` (for 3D objects) to detect `IInteractable` objects.
- Either the **agent (interactor)** or the **interactable** must have a **Rigidbody** component attached.
- Once detected, the interactor can invoke an interaction.

##### Customization Options for TriggerColliders:
- **Interactable Collider**: The specific collider component on the interactable object (2D or 3D).
- **LayerMask**: Defines which layers are valid for triggering an interaction.

---

## Complex Interaction Behavior

To implement more complex or responsive behaviors (e.g., performing validation checks before interaction):

- You can extend the `InteractionArgs` class.
- Pass an instance of the custom `InteractionArgs` when invoking the interaction to allow for more dynamic or parameterized interactions.

---

### Example Usage

Here’s an outline of how you might implement a simple interaction:

1. Create a class that implements `IInteractable`.
2. Create a class for your agent that implements `IInteractor`.
3. Customize the interactor to use either **raycasting** (ensure the interactable has a collider) or **trigger colliders** (ensure one of the objects has a rigidbody).
4. (Optional) For advanced behavior, create a custom `InteractionArgs` class and pass it during interaction invocations.

---

Happy Coding!
