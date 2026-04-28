import { Button } from "#/components/ui/button";
import { apiFetch } from "#/lib/api";
import { createFileRoute, ErrorComponent, Link } from "@tanstack/react-router";

type TestEndpoint = {
	message: string;
};
export const Route = createFileRoute("/")({
	loader: () =>
		apiFetch<TestEndpoint>("/Test", {
			headers: {
				Authorization: `Bearer ${import.meta.env.VITE_API_KEY}`,
			},
		}),
	errorComponent: ({ error }) => <ErrorComponent error={error} />,
	component: App,
});

function App() {
	const TestResponse = Route.useLoaderData();
	return (
		<main className="container mx-auto">
			<h1>{TestResponse.message}</h1>
			<div className="flex justify-center ">
				<Link to="/dashboard">
					<Button>Dashboard</Button>
				</Link>
			</div>
		</main>
	);
}
