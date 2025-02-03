document.addEventListener("DOMContentLoaded", () => {
    const ctx = document.getElementById('listingChart').getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug'],
            datasets: [{
                label: 'Listings Over Time',
                data: [120, 145, 140, 130, 135, 150, 155, 150],
                borderColor: '#ff7300',
                backgroundColor: 'rgba(255, 115, 0, 0.1)',
                tension: 0.3
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: { stepSize: 50 }
                }
            },
            plugins: { legend: { display: false } }
        }
    });

    function loadListings() {
        fetch('http://localhost:5240/listings')
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok (${response.status})`);
            }
            return response.json();
        })
        .then(listings => {
            const tbody = document.querySelector('.listings table tbody');
            tbody.innerHTML = '';
            listings.forEach(listing => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${listing.title}</td>
                    <td>${listing.address}</td>
                    <td>$${Number(listing.price).toLocaleString()}</td>
                    <td>${listing.areaInSquareMeters}</td>
                    <td>${listing.propertyType}</td>
                    <td>${new Date(listing.listingDate).toLocaleDateString()}</td>
                    <td><a href="/listings/${listing.id}" class="button">View</a></td>
                `;
                tbody.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Error fetching listings:', error);
        });
    }

    loadListings();

    const addListingButton = document.querySelector(".listing-button");
    const modal = document.getElementById("add-listing-modal");
    const closeBtn = document.querySelector(".close-btn");
    const form = document.getElementById("listingForm");

    addListingButton.addEventListener("click", (event) => {
        event.preventDefault();
        modal.style.display = "flex";
    });

    closeBtn.addEventListener("click", (event) => {
        event.preventDefault();
        form.reset();
        modal.style.display = "none";
    });

    window.addEventListener("click", (event) => {
        if (event.target === modal) {
            modal.style.display = "none";
            form.reset();
        }
    });

    async function uploadImage(file) {
        const formData = new FormData();
        formData.append("image", file);

        const response = await fetch("http://localhost:5240/upload-image", {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            throw new Error("Failed to upload image");
        }

        return await response.text();
    }

    form.addEventListener("submit", async function(event) {
        event.preventDefault();

        let imageUrls = [];
        const imageFiles = document.getElementById("images").files;

        for (let i = 0; i < imageFiles.length; i++) {
            let url = await uploadImage(imageFiles[i]);
            imageUrls.push(url);
        }

        const newListing = {
            title: document.getElementById("title").value,
            description: document.getElementById("description").value,
            address: document.getElementById("address").value,
            city: document.getElementById("city").value,
            state: document.getElementById("state").value,
            neighborhood: document.getElementById("neighborhood").value,
            price: parseFloat(document.getElementById("price").value),
            hOA: document.getElementById("hoa").value,
            propertyTaxes: document.getElementById("propertytaxes").value,
            areaInSquareMeters: parseFloat(document.getElementById("area").value),
            bedrooms: document.getElementById("bedrooms").value,
            bathrooms: document.getElementById("bathrooms").value,
            livingRoom: document.getElementById("livingroom").value,
            parkingSpaces: document.getElementById("parkingspaces").value,
            propertyType: document.getElementById("propertyType").value,
            images: imageUrls,
            listingDate: new Date().toISOString()
        };

        fetch('http://localhost:5240/listings', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newListing)
        })
        .then(response => response.json())
        .then(() => {
            loadListings();
            form.reset();
            modal.style.display = "none";
        })
        .catch(error => console.error("Error adding listing:", error));
    });
});