# UnambiguityChecker
The **UnambiguityChecker** is a class library designed to check the unambiguity conditions on pullback graphs and diagrams in the context of user interfaces (UIs). It is built on top of the **Categories** class library, which provides the necessary graph and category theory abstractions.

## Unambiguity Theory

### 3. User Interfaces as Pullbacks

User interfaces (UIs) can be represented as pullbacks in the category of directed labeled multigraphs (DLMG). A **pullback** is a universal construction that combines different components and their relationships while ensuring consistency and reducing ambiguity. By representing a UI as a pullback, we ensure that all the relationships between actions, states, and affordances are clearly defined.

### 4. Unambiguity of User Interfaces

**Unambiguity** refers to ensuring that user interactions within the interface are clear, deterministic, and consistent. An ambiguous interface can lead to confusion, errors, and poor user experience. To avoid ambiguity, each action or affordance in the UI must have a clear, predictable outcome. The following definitions and conditions help ensure that UIs are unambiguous.

#### Definitions and Theorems

1. **Local Injectivity**: A graph morphism f is said to be *locally injective* with respect to edge labels if:

   ![Local Injectivity](https://latex.codecogs.com/svg.latex?%5Cforall%20v%20%5Cin%20V_%7B%5CGamma%7D%20%5Cquad%20%5Cforall%20e_1%2C%20e_2%20%5Cin%20E%5E%7B%5Bv%2C%20-%5D%7D_%7B%5CGamma%7D%3A%20%5Cquad%20%5Clambda_%7B%5CGamma%7D%28e_1%29%20%5Cneq%20%5Clambda_%7B%5CGamma%7D%28e_2%29%20%5Cimplies%20f%5E%7Blabel%7D%28%5Clambda_%7B%5CGamma%7D%28e_1%29%29%20%5Cneq%20f%5E%7Blabel%7D%28%5Clambda_%7B%5CGamma%7D%28e_2%29%29)

   This ensures that edges with the same source vertex cannot have the same label unless they also share the same target vertex.

2. **Determinism of DLMG**: A directed labeled multigraph Gamma is *deterministic* if:

   ![Determinism of DLMG](https://latex.codecogs.com/svg.latex?%5Cforall%20v%20%5Cin%20V_%7B%5CGamma%7D%20%5Cquad%20%5Cforall%20e_1%2C%20e_2%20%5Cin%20E%5E%7B%5Bv%2C%20-%5D%7D_%7B%5CGamma%7D%3A%20%5Cquad%20%5Clambda_%7B%5CGamma%7D%28e_1%29%20%5Cneq%20%5Clambda_%7B%5CGamma%7D%28e_2%29%20%5Clor%20%5Ctau_%7B%5CGamma%7D%28e_1%29%20%3D%20%5Ctau_%7B%5CGamma%7D%28e_2%29)

   This ensures that if two edges from the same vertex have different labels, they must lead to the same target vertex, enforcing consistency in state transitions.

#### Unambiguity of a UI Pullback

A user interface (UI) represented as a **pullback graph** P over a diagram
![Link to Formula](https://latex.codecogs.com/svg.latex?A%20%5C%5C%20%5Ctext%7Bf%7D%20%5Cto%20M%20%5Cleftarrow%20g%20%5C%5C%20D)

with morphisms pi_A and pi_D is **unambiguous** if the following conditions hold:

- For any two edges e_1 and e_2 in the pullback graph P with the same source in state s in V_A, if their labels are equal, their targets must be in the same state s' in V_A.

- If the edges e_1 and e_2 have the same source, their labels must match, and their targets must also be identical.

This ensures that there is no confusion or uncertainty in the actions that the UI affords to the user.

#### Theorem: Unambiguity of an Interface Pullback

A UI pullback P is unambiguous if and only if the following **diagram conditions** hold:

1. **Uniqueness**: For any edge e_A in E_A, there exists no more than one edge e_D in E_D such that the edge labels match.

2. **Splitting Coherence**: If two edges
   ![Link to Formula](https://latex.codecogs.com/svg.latex?e_1%5E%7BA%7D%2C%20e_2%5E%7BA%7D%20%5Cin%20E_A%5E%7B%5Bv_A%2C%20-%5D%7D)
have different targets in the action graph A, then:

   ![Splitting Coherence](https://latex.codecogs.com/svg.latex?%5Ctau_A%28e_1%5E%7BA%7D%29%20%5Cneq%20%5Ctau_A%28e_2%5E%7BA%7D%29%20%5Cimplies%20f%5E%7Bedge%7D%28e_1%5E%7BA%7D%29%20%5Cneq%20f%5E%7Bedge%7D%28e_2%5E%7BA%7D%29%20%5Cquad%20%5Cland%20%5C%5Clambda_D%28e_1%5E%7BD%7D%29%20%5Cneq%20%5Clambda_D%28e_2%5E%7BD%7D%29)

   This ensures that distinct transitions between states are always represented by distinct affordances in the affordance graph $D$.

These conditions help guarantee that actions within the UI are consistent, without any conflicting or redundant effects.

### Strict Unambiguity

In some interfaces, only one representation exists for each state. Such interfaces are often called **single-context interfaces**. In these interfaces, each state has a unique affordance, and no affordance leads to multiple transitions from the same state. This provides a **strict unambiguity**.

#### Definition: Single-Context Interface

Let P be **pullback graph** over a diagram
![LinkDiagram](https://latex.codecogs.com/svg.latex?A%20%5C%5C%20%5Ctext%7Bf%7D%20%5Cto%20M%20%5Cleftarrow%20g%20%5C%5C%20D)

with morphisms pi_A and pi_D. P is a **single-context interface** if:

![Single-Context Interface](https://latex.codecogs.com/svg.latex?%5Cforall%20v_1%2C%20v_2%20%5Cin%20V_P%3A%20%5Cquad%20%5Cpi_A%5E%7Bvertex%7D%28v_1%29%20%3D%20%5Cpi_A%5E%7Bvertex%7D%28v_2%29%20%5Cimplies%20%5Cpi_D%5E%7Bvertex%7D%28v_1%29%20%3D%20%5Cpi_D%5E%7Bvertex%7D%28v_2%29)

This condition ensures that each state is uniquely represented by a single context in the interface.

#### Proposition: Single-Context Interface

A pullback graph P is a **single-context interface** if and only if the morphism g is injective on vertices.

### When Ambiguity is Detected

Unambiguity conditions help identify and address ambiguities in interface design. Some sources of ambiguity include:

1. **Unrecognizable States**: If a user cannot distinguish between states where the same affordance has different effects, the interface becomes ambiguous. Additional design elements should be introduced to allow users to differentiate between states.

2. **Hidden Complexities**: Internal dependencies between states and contexts can lead to ambiguity if users are unaware of them. These dependencies should be made explicit to avoid confusion.

3. **Limited Number of Affordances**: When a single affordance performs multiple actions, it increases the cognitive load on users. It is recommended to introduce more affordances or representations to simplify the interface and make it more intuitive.

By understanding and addressing these sources of ambiguity, we can design interfaces that are more clear, intuitive, and unambiguous for users.



## Installation

To use the **UnambiguityChecker** class library in your project, follow these steps:

### 1. Clone the Repository

Clone this repository to your local machine:

```bash
git clone https://github.com/mzheplin/UIGraphs.git
cd UIGraphs
```
### 2. Build and Reference Dependencies
The UnambiguityChecker library depends on the Categories class library. You must first build the Categories project and reference it in UnambiguityChecker.

1. Navigate to the Categories directory:
```bash
cd Categories
```
2. Build the project:
```bash
dotnet build
```
3. In the UnambiguityChecker directory, add the reference to the Categories project:
```bash
cd ../UnambiguityChecker
dotnet add reference ../Categories/Categories.csproj
```

## Usage

After setting up the project, you can use the UnambiguityChecker class to check unambiguity conditions on pullback graphs and diagrams.

#### Example
```csharp
using Categories;
using UnambiguityChecker;

Vertex a = new Vertex("a");
Vertex b = new Vertex("b");
Vertex c = new Vertex("c");
Vertex d = new Vertex("d");

List<DEdge> edges1 = new List<DEdge>() {
new DEdge(b, a, "a"),
new DEdge(b, a, "b"),
};

DLMGraph g_1 = new DLMGraph(edges1);

List<DEdge> edgesc = new List<DEdge>() {
new DEdge(a, b, "a"),
new DEdge(c, b, "b"),
new DEdge(c, d, "c")
};

DLMGraph g_c = new DLMGraph(edgesc);

List<DEdge> edges2 = new List<DEdge>() {
new DEdge(a, b, "a"),
new DEdge(c, b, "c"),
new DEdge(c, b, "d")
};

DLMGraph g_2 = new DLMGraph(edges2);

Dictionary<Vertex, Vertex> v_c1 = new Dictionary<Vertex, Vertex>()
{ {a,b },{c,b},{b,a},{d,a }};
Dictionary<string, string> l_c1 = new Dictionary<string, string>()
{{"a","a" },{"b","b"},{"c","a"} };
Dictionary<Vertex, Vertex> v_21 = new Dictionary<Vertex, Vertex>()
{{a,b },{c,b},{b,a}};
Dictionary<string, string> l_21 = new Dictionary<string, string>()
{{"a","a" },{"d","b"},{"c","a"} };
DLMGHomomorphism h1 = new DLMGHomomorphism(g_c, g_1, v_c1, l_c1);
DLMGHomomorphism h2 = new DLMGHomomorphism(g_2, g_1, v_21, l_21);

DLMGCategory category = new DLMGCategory();
var pullback = category.GetPullback(h1,h2); 

// Check interface conditions
UnambiguityChecker checker = new UnambiguityChecker();
bool generalConditions = checker.IsPullbackUnambiguous(pullback);
Console.WriteLine($"General Interface Conditions: {generalConditions}");

// Check diagram conditions
bool diagramConditions = checker.CheckDiagramConditions(h1, h2);
Console.WriteLine($"Diagram Conditions: {diagramConditions}");
```