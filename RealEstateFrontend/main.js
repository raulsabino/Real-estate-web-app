document.addEventListener("DOMContentLoaded", () => {
    let currentListingId = null;
    
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
                    <td><a href="#" class="button view-btn" data-id="${listing.id}">View</a></td>
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
    const updateModal = document.getElementById("update-listing-modal");
    const addCloseBtn = document.querySelector(".add-close-btn");
    const updateCloseBtn = document.querySelector(".update-close-btn");
    const form = document.getElementById("listingForm");
    const viewButton = document.querySelector('.listings table tbody');

    //create

    addListingButton.addEventListener("click", (event) => {
        event.preventDefault();
        modal.style.display = "flex";
    });

    addCloseBtn.addEventListener("click", (event) => {
        event.preventDefault();
        modal.style.display = "none";
        form.reset();
    });

    window.addEventListener("click", (event) => {
        if (event.target === modal) {
            modal.style.display = "none";
            form.reset();
        }
    });

    //update

    viewButton.addEventListener('click', (event) => {
        if (event.target && event.target.matches('a.view-btn')) {
            event.preventDefault();
            
            updateModal.style.display = "flex";
    
            const listingId = event.target.getAttribute('data-id');
            currentListingId = listingId;
    
            fetch(`http://localhost:5240/listings/${listingId}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`Failed to fetch listing: ${response.status}`);
                    }
                    return response.json();
                })
                .then(listing => {
                    if (!listing) {
                        console.error("Error: Listing is null or undefined.");
                        return;
                    }

                    document.getElementById('update-title').value = listing.title || '';
                    document.getElementById('update-description').value = listing.description || '';
                    document.getElementById('update-address').value = listing.address || '';
                    document.getElementById('update-city').value = listing.city || '';
                    document.getElementById('update-state').value = listing.state || '';
                    document.getElementById('update-neighborhood').value = listing.neighborhood || '';
                    document.getElementById('update-price').value = listing.price || 0;
                    document.getElementById('update-hoa').value = listing.hoa || 0;
                    document.getElementById('update-propertytaxes').value = listing.propertyTaxes || 0;
                    document.getElementById('update-area').value = listing.areaInSquareMeters || 0;
                    document.getElementById('update-bedrooms').value = listing.bedrooms || 0;
                    document.getElementById('update-bathrooms').value = listing.bathrooms || 0;
                    document.getElementById('update-livingroom').value = listing.livingRoom || 0;
                    document.getElementById('update-parkingspaces').value = listing.parkingSpaces || 0;
                    document.getElementById('update-propertyType').value = listing.propertyType || '';
                })
                .catch(error => {
                    console.error("Error fetching listing details:", error);
                });
        }
    });
    

    updateCloseBtn.addEventListener("click", (event) => {
        event.preventDefault();
        updateModal.style.display = "none";
        form.reset();
    });

    window.addEventListener("click", (event) => {
        if (event.target === updateModal) {
            updateModal.style.display = "none";
            form.reset();
        }
    });

    //create

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

    // delete
    
    const deleteButton = document.querySelector(".deleteButton");
    deleteButton.addEventListener("click", (event) => {
        if (!currentListingId) {
        alert("No listing ID found. Unable to delete.");
        return;
        }
        
        const confirmDelete = confirm("Are you sure you want to delete this listing?");
        if (!confirmDelete) {
        return;
        }
        
        fetch(`http://localhost:5240/listings/${currentListingId}`, {
        method: "DELETE",
        })
        .then(response => {
            if (!response.ok) {
            throw new Error(`Failed to delete listing: ${response.status}`);
            }
            alert("Listing deleted successfully!");
            updateModal.style.display = "none";
            loadListings();
            currentListingId = null;
        })
        .catch(error => {
            console.error("Error deleting listing:", error);
            alert("Failed to delete the listing. Please try again.");
        });
    });
});