let metadata;
let project = {};

window.onload = async () => {
    metadata =
        await fetch("/metadata")
            .then(r => r.json());

    buildTables();

    await openProject();
};

function buildTables() {
    const div =
        document.getElementById("tables");

    div.innerHTML = "";

    for (const [name, table] of Object.entries(metadata.Collections)) {
        buildTable(div, name, table);
    }
}

function buildTable(parent, name, table) {
    const div =
        document.createElement("div");

    div.innerHTML =
        `
<h2>${table.Title}</h2>

<table>

<thead>

<tr>

${table.Columns
            .map(c => `<th>${c.Label}</th>`)
            .join("")}

<th></th>

</tr>

</thead>

<tbody id="${name}Body">
</tbody>

</table>

<button onclick="addRow('${name}')">

+ Add

</button>
`;

    parent.appendChild(div);
}

function addRow(name, values = {}) {
    const table =
        metadata.Collections[name];

    const row =
        document.createElement("tr");

    row.innerHTML =
        table.Columns
            .map(c => {
                const value =
                    values[c.Property] ?? "";

                return `
<td>
<input
class="${c.Property}"
type="${c.Type}"
value="${value}">
</td>`;
            })
            .join("")
        +
        `
<td>

<button onclick="this.closest('tr').remove()">

X

</button>

</td>
`;

    document
        .getElementById(name + "Body")
        .appendChild(row);
}

async function openProject() {
    project =
        await fetch("/project")
            .then(r => r.json());

    populate(project);
}

async function saveProject() {
    project = collect();

    const response =
        await fetch("/project",
            {
                method: "POST",

                headers:
                {
                    "Content-Type": "application/json"
                },

                body: JSON.stringify(project)
            });

    if (response.ok)
        alert("Saved.");
}

function collect() {
    const result = {};

    for (const [name, table] of Object.entries(metadata.Collections)) {
        result[name] = getRows(name, table);
    }

    return result;
}

function getRows(name, table) {
    return [...document.querySelectorAll(`#${name}Body tr`)]
        .map(row => {
            const obj = {};

            table.Columns.forEach(c => {
                let value =
                    row.querySelector("." + c.Property).value;

                if (c.Type === "number")
                    value = Number(value);

                obj[c.Property] = value;
            });

            return obj;
        });
}

function populate(project) {
    for (const [name, table] of Object.entries(metadata.Collections)) {
        const body =
            document.getElementById(name + "Body");

        body.innerHTML = "";

        const rows =
            project[name] ?? [];

        rows.forEach(r => addRow(name, r));
    }
}