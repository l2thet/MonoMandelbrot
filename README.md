# MonoMandelbrot

MonoMandelbrot is a .NET 8 desktop application that visualizes the Mandelbrot fractal using the MonoGame framework. It provides an interactive window for exploring the Mandelbrot set with smooth zooming and view reset capabilities.

## Features

- High-resolution Mandelbrot fractal rendering (1200x1200 pixels)
- Interactive zoom: Left-click to zoom in on any point
- View reset: Right-click to reset the view to the default
- Multi-core fractal generation for fast rendering
- Colorful visualization based on iteration counts

## Usage

1. Run the application.
2. Left-click anywhere on the fractal to zoom in at that location.
3. Right-click to reset the view to the original state.
4. Press `Esc` or the gamepad's Back button to exit.

## Technologies

- .NET 8
- MonoGame framework
- C#
- Parallel processing for fast fractal generation

## Building

1. Ensure you have .NET 8 SDK and MonoGame installed.
2. Open the solution in Visual Studio 2022.
3. Build and run the project.

## License

This project is open source. See the [LICENSE](LICENSE) file for details.