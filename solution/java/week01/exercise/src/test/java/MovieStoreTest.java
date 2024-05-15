import static org.junit.Assert.*;

import movie.Movie;
import movie.MovieStore;
import org.junit.*;

import java.util.List;

public class MovieStoreTest {
    private MovieStore store;

    //Tests interdependency: the setup assumes a set of movies for all tests which makes them prone to fail.
    @Before
    public void setUp() {
        store = new MovieStore();
        store.addMovie("001", "Inception", "Christopher Nolan", 10, 0d);
        store.addMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 8, 0d);
        store.addMovie("003", "Dunkirk", "Christopher Nolan", 5, 0d);
    }

    @Test
    public void testAddMovie() {
        store.addMovie("002", "The Matrix", "Lana Wachowski, Lilly Wachowski", 8, 0d);
        //Weak assertions: Checks for not null but not for correct properties
        assertNotNull("Movie should not be null", store.allMovies.get("002"));
        //Implementation leak: tight to the implementation details of MovieStore
        assertEquals("Incorrect count of total copies", 8, store.allMovies.get("002").totalCopies);
    }

    @Test
    public void testRemoveMovie() {
        store.removeMovie("001");
        assertNull("Movie should be removed", store.allMovies.get("001"));
    }

    @Test
    public void testBorrowMovie() {
        store.borrowMovie("001");
        assertEquals("Borrowed copies should be incremented", 1, store.allMovies.get("001").borrowedCopies);
    }

    @Test
    public void testBuyMovie() {
        var movie = store.allMovies.get("001");
        //Mutable state between tests: changing global set for a specific state
        movie.unitPrice = 5d;

        store.buyMovie("Durant", "001");

        assertEquals("Movie bought should decrease all copies", 9, store.allMovies.get("001").totalCopies);
    }

    @Test
    public void testReturnMovie() {
        store.returnMovie("001");
        assertEquals("Borrowed copies should be decremented", 0, store.allMovies.get("001").borrowedCopies);
    }

    @Test
    public void testFindMoviesByTitle() {
        List<Movie> movies = store.findMoviesByTitle("Inception");
        assertEquals("Should find one movie", 1, movies.size());
        assertEquals("Movie title should be 'Inception'", "Inception", movies.get(0).title);
    }
}
