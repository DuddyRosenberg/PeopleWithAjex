$(() => {
    loadTable();

    function loadTable() {
        $.get('/home/GetPeople', function (people) {
            $('#table-body tr').remove();
            people.forEach((person) => {
                $('#table-body').append
                    (`<tr>
                         <td>${person.firstName}</td>
                         <td>${person.lastName}</td>
                         <td>${person.age}</td>
                         <td>
                            <button class="btn btn-warning edit-btn" data-id="${person.id}">Edit</button>
                            <button class="btn btn-danger delete-btn" data-id="${person.id}">Delete</button>
                         </td>
                      </tr>`);
            });
        });
    }

    $('#add-person').click(function () {
        person = {
            firstName: $('#first-name').val(),
            lastName: $('#last-name').val(),
            age: $('#age').val(),
        }
        $.post('home/AddPerson', person, function (object) {
            if (object.success) {
                $('#first-name').val('');
                $('#last-name').val('');
                $('#age').val('');
                loadTable();
            }
        });
    });

    $('#table-body').on('click', '.edit-btn', function () {
        let id = $(this).data('id');
        $.get(`home/getPerson?id=${id}`, function (person) {
            $('#first-name-edit').val(person.firstName);
            $('#last-name-edit').val(person.lastName);
            $('#age-edit').val(person.age);
            $('#id-edit').val(person.id);
        });
        $('#edit-modal').modal();
    });

    $('#submit-edit').click(function () {
        person = {
            firstName: $('#first-name-edit').val(),
            lastName: $('#last-name-edit').val(),
            age: $('#age-edit').val(),
            id: $('#id-edit').val(),
        }
        $.post('/home/EditPerson', person, function (object) {
            $('#edit-modal').modal('hide');
            loadTable();
        });
    });

    $('#table-body').on('click', '.delete-btn', function () {
        let id = $(this).data('id');
        $.post('home/deletePerson', { id, }, function (object) {
            loadTable();
        });
    });
});