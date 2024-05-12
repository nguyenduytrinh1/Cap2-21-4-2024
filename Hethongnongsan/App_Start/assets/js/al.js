function checks() {
    var preview = document.getElementById('preview');
    preview.innerHTML = ''; // Clear previous preview

    // Lấy tệp tin từ trường input
    var fileInputs = document.getElementsByClassName('check-img');
    var file;

    // Lặp qua tất cả các phần tử
    for (var i = 0; i < fileInputs.length; i++) {
        var fileInput = fileInputs[i];
        if (fileInput.files.length > 0) {
            file = fileInput.files[0];
            break; // Thoát khỏi vòng lặp sau khi tìm thấy tệp tin đầu tiên
        }
    }

    // Kiểm tra xem đã chọn tệp tin hay chưa
    if (!file) {
        console.log('Vui lòng chọn một tệp tin.');
        return;
    }

    // Hiển thị ảnh dưới dạng preview
    var reader = new FileReader();
    reader.onload = function (event) {
        var img = document.createElement('img');
        img.src = event.target.result;
        img.style.maxWidth = '100%';
        preview.appendChild(img);
    };
    reader.readAsDataURL(file);

    // Đọc tệp tin dưới dạng base64 khi tệp tin được chọn
    reader.onloadend = function (event) {
        var base64String = event.target.result;
        var base64ImageData = base64String.split(',')[1]; // Lấy phần base64

        // Gửi dữ liệu base64 đến API Flask để dự đoán
        fetchPrediction(base64ImageData);
    };
}

function fetchPrediction(base64ImageData) {
    // Tạo một đối tượng FormData để chứa dữ liệu base64
    var formData = new FormData();
    formData.append('image', base64ImageData);

    // Lấy thẻ nút submit
    var submitButton = document.querySelector('input[type="submit"]');

    // Gửi yêu cầu POST đến API Flask
    fetch('http://127.0.0.1:5000/predict', {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
        .then(data => {
            console.log(data); // In ra dữ liệu trả về từ API
            // Kiểm tra nếu dự đoán là "rotten"
            if (data.prediction === "rotten") {
                alert("Trái cây không được tươi. Vui lòng chọn ảnh khác");
                // Ẩn nút submit
                submitButton.style.display = 'none';
            } else {
                // Hiện nút submit nếu không phải dự đoán "rotten"
                submitButton.style.display = 'block';
            }
            // Xử lý dữ liệu trả về theo nhu cầu của bạn
        })
        .catch(error => {
            console.error('There was a problem with your fetch operation:', error);
            // Xử lý khi gặp lỗi
        });
}
