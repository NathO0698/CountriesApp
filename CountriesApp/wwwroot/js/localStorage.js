window.localStorageHelper = {
    saveFavorites: function (favorites) {
        localStorage.setItem('favoriteCountries', JSON.stringify(favorites));  // Sauvegarde les favoris dans localStorage
    },
    loadFavorites: function () {
        var favorites = localStorage.getItem('favoriteCountries');  // Récupère les favoris
        return favorites ? JSON.parse(favorites) : [];  // Si des favoris existent, les retourne, sinon retourne un tableau vide
    },
    removeFavorite: function (name) {
        var favorites = JSON.parse(localStorage.getItem('favoriteCountries')) || [];
        favorites = favorites.filter(function(fav) { return fav.name !== name; });  // Supprime le pays du tableau de favoris
        localStorage.setItem('favoriteCountries', JSON.stringify(favorites));  // Met à jour localStorage
    }
};