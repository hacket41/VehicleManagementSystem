const URL = import.meta.env.VITE_API;

export async function apiFetch<T>(
	path: string,
	init?: RequestInit,
): Promise<T> {
	try {
		const res = await fetch(`${URL}${path}`, {
			...init,
			headers: {
				"Content-Type": "application/json",
				...init?.headers,
			},
		});

		const contentType = res.headers.get("content-type");

		let data: unknown = null;

		if (contentType?.includes("application/json")) {
			data = await res.json();
		} else {
			data = await res.text();
		}

		if (!res.ok) {
			if (typeof data === "object" && data !== null) {
				const problem = data as any;

				throw new Error(problem.detail || problem.title || `API ${res.status}`);
			}

			throw new Error(
				typeof data === "string"
					? data
					: `API ${res.status}: ${res.statusText}`,
			);
		}

		return data as T;
	} catch (err) {
		if (err instanceof TypeError) {
			throw new Error("Network error: Unable to reach server");
		}

		throw err;
	}
}
